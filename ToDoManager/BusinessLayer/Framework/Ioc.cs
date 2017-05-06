using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shared.CustomAttributes;

namespace BusinessLayer.Framework
{
    public static class Ioc
    {
        private static readonly Dictionary<Type, Type> _typesByInterface;
        private static readonly Dictionary<Type, object> _instanceByInterface;

        static Ioc()
        {
            _typesByInterface = new Dictionary<Type, Type>();
            _instanceByInterface = new Dictionary<Type, object>();
        }

        public static void Initialise()
        {
            var types = GetTypes();
            var bulkLoadInterfaceTypes = types.Where(t => t.IsInterface && t.GetCustomAttribute<IocBulkLoadAttribute>() != null);

            foreach (var interfaceType in bulkLoadInterfaceTypes)
            {
                var inheritingType = types.FirstOrDefault(t => !t.IsInterface && interfaceType.IsAssignableFrom(t));

                if (inheritingType != null)
                    _typesByInterface.Add(interfaceType, inheritingType);
            }
        }

        private static Type[] GetTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(a => a.GetTypes()).ToArray();
            return types;
        }

        public static TInterface Get<TInterface>() where TInterface : class
        {
            var type = typeof(TInterface);

            if (!_instanceByInterface.ContainsKey(type))
                _instanceByInterface.Add(type, CreateInstance<TInterface>());

            return (TInterface) _instanceByInterface[type];
        }

        private static TInterface CreateInstance<TInterface>() where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            if (!_typesByInterface.ContainsKey(interfaceType))
            {
                var types = GetTypes();
                var inheritingType = types.FirstOrDefault(t => !t.IsInterface && interfaceType.IsAssignableFrom(t));
                
                if (inheritingType == null)
                    throw new Exception($"Failed to find the inheriting type of {interfaceType.Name}.");

                _typesByInterface.Add(interfaceType, inheritingType);
            }

            var type = _typesByInterface[interfaceType];

            return (TInterface) Activator.CreateInstance(type);
        }
    }
}