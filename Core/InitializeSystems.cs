using System.Collections.Generic;

namespace Core
{
    public class InitializeSystems
    {
        readonly List<IInitializeSystem> initializeSystems = new List<IInitializeSystem>();

        public void Add(IInitializeSystem initializeSystem)
        {
            initializeSystems.Add(initializeSystem);
        }

        public void Initialize()
        {
            foreach (var system in initializeSystems)
            {
                system.Initialize();
            }
        }
    }
}
