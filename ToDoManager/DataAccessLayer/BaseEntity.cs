using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataAccessLayer.CustomAttributes;
using Shared.Enums;

namespace DataAccessLayer
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime? DateUpdated { get; set; }
        string UpdateUsername { get; set; }
    }

    public class BaseEntity<TEntity> where TEntity : BaseEntity<TEntity>, IEntity
    {
        private readonly ConnectionStrings _connectionString;
        private EntityStates _state;
        private Dictionary<string, object> _valuesByPropertyName;

        protected BaseEntity()
        {
            var type = GetType();

            _state = EntityStates.New;
            _connectionString = GetType().GetCustomAttribute<DalTable>().ConnectionString;
            _valuesByPropertyName = type.GetProperties().ToDictionary(p => p.Name, p => GetDefault(p.PropertyType));
        }

        protected BaseEntity(int id) : this()
        {
            Id = id;
            _state = EntityStates.ToLoad;
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdateUsername { get; set; }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("The property name was not provided.");

            if (_state != EntityStates.ToLoad)
                return (T) _valuesByPropertyName[propertyName];

            using (var context = new CustomDbContext<TEntity>(_connectionString))
            {
                var entity = context.Entities.Find(Id);
                if (entity != null)
                    _valuesByPropertyName = entity.GetType()
                        .GetProperties()
                        .ToDictionary(p => p.Name, p => p.GetValue(entity));
            }

            _state = EntityStates.Loaded;

            return (T) _valuesByPropertyName[propertyName];
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("The property name was not provided.");

            _valuesByPropertyName[propertyName] = value;

            _state = EntityStates.Dirty;
        }

        public void Save()
        {
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}