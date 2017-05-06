using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using Shared.Enums;

namespace DataAccessLayer
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime? DateUpdated { get; set; }
        string UpdateUsername { get; set; }
        void Save();
        void Delete();
    }

    public class Entity<TEntity> where TEntity : Entity<TEntity>, IEntity
    {
        private EntityStates _state;
        private Dictionary<string, object> _valuesByPropertyName;

        protected Entity()
        {
            var type = GetType();

            _state = EntityStates.New;
            _valuesByPropertyName = type.GetProperties().ToDictionary(p => p.Name, p => GetDefault(p.PropertyType));
        }

        protected Entity(int id) : this()
        {
            Id = id;
            _state = EntityStates.ToLoad;
        }

        public int Id { get; set; }

        public DateTime DateCreated
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public DateTime? DateUpdated
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public string UpdateUsername
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("The property name was not provided.");

            if (_state == EntityStates.ToLoad)
                Load();

            return (T) _valuesByPropertyName[propertyName];
        }

        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("The property name was not provided.");

            if (_state == EntityStates.ToLoad)
                Load();

            _valuesByPropertyName[propertyName] = value;

            _state = EntityStates.Dirty;
        }

        public void Save()
        {
            if (Id == 0)
                DateCreated = DateTime.Now;

            DateUpdated = DateTime.Now;
            UpdateUsername = Environment.UserName;

            using (var context = new CustomDbContext<TEntity>())
            {
                context.Entities.AddOrUpdate((TEntity) this);
                context.SaveChanges();
            }
        }

        public void Delete()
        {
            if (Id == 0)
                return;

            using (var context = new CustomDbContext<TEntity>())
            {
                var entity = context.Entities.Find(Id);
                
                if (entity == null)
                    throw new Exception($"Could not find an {GetType().Name} entity with Id {Id}.");

                context.Entities.Remove(entity);
                context.SaveChanges();
            }
        }

        private void Load()
        {
            using (var context = new CustomDbContext<TEntity>())
            {
                var entity = context.Entities.Find(Id);

                if (entity == null)
                    throw new Exception($"Could not find an {GetType().Name} entity with Id {Id}.");

                _valuesByPropertyName = entity.GetType()
                    .GetProperties()
                    .ToDictionary(p => p.Name, p => p.GetValue(entity));
            }

            _state = EntityStates.Loaded;
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}