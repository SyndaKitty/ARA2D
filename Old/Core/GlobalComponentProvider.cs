using System;
using System.Collections.Generic;
using Nez;

namespace ARA2D.Core
{
    public class GlobalComponentProvider : IComponentProvider
    {
        readonly Dictionary<Type, Component> cachedComponents = new Dictionary<Type, Component>();

        public void CacheComponent<T>(T component) where T : Component
        {
            cachedComponents.Add(typeof(T), component);
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)cachedComponents[typeof(T)];
        }
    }
}
