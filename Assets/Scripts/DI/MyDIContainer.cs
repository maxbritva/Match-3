using System;
using System.Collections.Generic;

namespace DI
{
    public class MyDIContainer
    {
        private readonly MyDIContainer _parentContainer;
        private readonly HashSet<(string, Type)> _resolutions = new HashSet<(string, Type)>();
        private readonly Dictionary<(string, Type), DiRegistration> _registrations = 
            new Dictionary<(string, Type), DiRegistration>();

        public MyDIContainer(MyDIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }


        public void RegisterSingleton<T>(Func<MyDIContainer, T> factory) => RegisterSingleton(null, factory);
        
        public void RegisterTransient<T>(Func<MyDIContainer, T> factory) => RegisterTransient(null, factory);
        
        public void RegisterInstance<T>(T instance) => RegisterInstance(null, instance);

        public void RegisterSingleton<T>(string tag, Func<MyDIContainer, T> factory) => 
            Register((tag, typeof(T)), factory, true);

        public void RegisterTransient<T>(string tag, Func<MyDIContainer, T> factory) => 
            Register((tag, typeof(T)), factory, false);

        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));
            if(_registrations.ContainsKey(key))
                throw new Exception($"tag {key.Item1} with type {key.Item2.FullName}  has already registered");

            _registrations[key] = new DiRegistration
            {
             Instance = instance,
             IsSingleton = true
            };
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutions.Contains(key))
                throw new Exception($"Cyclic dependency for tag {key.tag} and type {key.Item2.FullName}");

            _resolutions.Add(key);


            try
            {
                if (_registrations.TryGetValue(key, out var registration))
                {
                    if (registration.IsSingleton == false) return (T) registration.Factory(this);
                    if (registration.Instance == null && registration.Factory != null)
                        registration.Instance = registration.Factory(this);
                    return (T) registration.Instance;
                }
                else
                {
                    if (_parentContainer != null)
                        return _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutions.Remove(key);
            } 
            throw new Exception($"Couldn't find dependency for tag {key.tag} and type {key.Item2.FullName}");
        }

        private void Register<T>((string, Type) key, Func<MyDIContainer, T> factory, bool isSingleton)
        {
            if(_registrations.ContainsKey(key))
                throw new Exception($"tag {key.Item1} with type {key.Item2.FullName}  has already registered");

            _registrations[key] = new DiRegistration
            {
                Factory = c => factory(c), IsSingleton = isSingleton
            };
        }
    }
}