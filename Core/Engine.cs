using Core.ServiceRegistration;
using DefaultEcs;

namespace Core
{
    public class Engine
    {
        World world;

        public Engine(Services services)
        {
            World world = new World();

            var serviceRegistrationSystem = new ServiceRegistrationSystems(world, services);
            serviceRegistrationSystem.Initialize();

        }
    }
}
