using System.Collections.Generic;
using DefaultEcs.System;

namespace Core
{
    /// <summary>
    /// A group of systems that can all be executed on multiple threads in parallel
    /// </summary>
    public class ParallelSystems<T> : ISystem<T>
    {
        // TODO: Write multi-threaded code to run systems in parallel
        // Right now this is basically a reimplementation of SequentialSystem

        readonly List<ISystem<T>> systems = new List<ISystem<T>>();

        public bool IsEnabled { get; set; }
        
        public void Add(ISystem<T> system)
        {
            systems.Add(system);
        }

        public void Update(T state)
        {
            if (!IsEnabled) return;
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Update(state);
            }
        }

        public void Dispose()
        {
            for (int i = systems.Count - 1; i >= 0; i--)
            {
                systems[i].Dispose();
            }
        }
    }
}
