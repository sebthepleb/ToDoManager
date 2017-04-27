using System;
using System.Collections.Generic;

namespace BusinessLayer.Framework
{
    public static class Ioc
    {
        private static readonly Dictionary<Type, object> _instanceByType;

        static Ioc()
        {
            _instanceByType = new Dictionary<Type, object>();
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);

            if (!_instanceByType.ContainsKey(type))
                _instanceByType.Add(type, Activator.CreateInstance<T>());

            return (T) _instanceByType[type];
        }
    }
}