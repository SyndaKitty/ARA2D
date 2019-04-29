using Core.Contexts;
using Entitas;

namespace Core.ServiceRegistration
{
    public class RegisterCreateViewService : IInitializeSystem
    {
        readonly MetaContext metaContext;
        readonly ICreateViewService createViewService;

        public RegisterCreateViewService(GenEntitas.Contexts contexts, ICreateViewService createViewService)
        {
            //metaContext = contexts.meta;
            this.createViewService = createViewService;
        }

        public void Initialize()
        {
            
        }
    }
}
