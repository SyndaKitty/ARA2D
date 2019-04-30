using DefaultEcs;

namespace Core.ServiceRegistration
{
    public class ServiceRegistrationSystems : InitializeSystems
    {
        public ServiceRegistrationSystems(World world, Services services)
        {
            Add(new RegisterCreateViewService(world, services.CreateView));
        }
    }
}
