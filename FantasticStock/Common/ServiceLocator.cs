using System;
using System.Collections.Generic;

namespace FantasticStock.Common
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public static void Register<T>(object implementation)
        {
            _services[typeof(T)] = implementation;
        }
        
        public static T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
        }
    }
}