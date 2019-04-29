using GenEntitas;

namespace Core.ServiceRegistration
{
    public class ServiceRegistrationSystems : Feature
    {
        public ServiceRegistrationSystems(GenEntitas.Contexts contexts, Services services)
        {
            Add(new RegisterCreateViewService(contexts, services.CreateView));
        }
    }
}
