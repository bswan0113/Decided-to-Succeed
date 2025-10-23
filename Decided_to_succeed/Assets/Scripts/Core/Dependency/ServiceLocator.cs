using System;
using System.Collections.Generic;
using Core.Logging;
using UnityEngine.SceneManagement;

namespace Core.Dependency
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, object> _sceneServices = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                CLogger.LogWarning($"[ServiceLocator] Service of type '{type.Name}' is already registered. It will be overwritten.");
                _services[type] = service;
            }
            else
            {
                _services.Add(type, service);
            }
        }
        public static void RegisterScene<T>(T service)
        {
            var type = typeof(T);
            if (_sceneServices.ContainsKey(type))
            {
                _sceneServices.Remove(type);
            }
            if (_sceneServices.ContainsKey(type))
            {
                CLogger.LogWarning(
                    $"[ServiceLocator] Scene Service of type '{type.Name}' is already registered for the current scene. It will be overwritten.");
            }
            _sceneServices[type] = service;
        }
        public static void OnSceneUnloaded(Scene current)
        {
            _sceneServices.Clear();
            CLogger.Log($"[ServiceLocator] Scene services cleared due to unload of scene: {current.name}");
        }
        public static T Get<T>()
        {
            var type = typeof(T);
            if (!_services.TryGetValue(type, out var service))
            {
                CLogger.LogError($"[ServiceLocator] Service of type '{type.Name}' is not registered.");
                throw new InvalidOperationException($"Cannot find a service of type {type.Name}.");
            }
            return (T)service;
        }
        public static T GetSceneInstance<T>()
        {
            var type = typeof(T);
            if (!_sceneServices.TryGetValue(type, out var service))
            {
                CLogger.LogError($"[ServiceLocator] Service of type '{type.Name}' is not registered.");
                throw new InvalidOperationException($"Cannot find a service of type {type.Name}.");
            }
            return (T)service;
        }
        public static void Clear()
        {
            _services.Clear();
        }
    }
}