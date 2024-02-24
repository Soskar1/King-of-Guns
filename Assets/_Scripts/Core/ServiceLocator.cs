using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class ServiceLocator
    {
        private static ServiceLocator instance = null;
        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServiceLocator();
                
                return instance;
            }
        }

        private Dictionary<string, IService> _services = new Dictionary<string, IService>();

        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }

        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogWarning($"Attempted to register service of type {key} which is already registered with the {GetType().Name}");
                return;
            }

            _services.Add(key, service);
        }
    }
}