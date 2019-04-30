using DefaultEcs;

namespace Core.ServiceRegistration
{
    public class RegisterCreateViewService : IInitializeSystem
    {
        readonly ICreateViewService createViewService;
        readonly World world;

        public RegisterCreateViewService(World world, ICreateViewService createViewService)
        {
            this.world = world;
            this.createViewService = createViewService;
        }

        public void Initialize()
        {
            world.SetMaximumComponentCount<CreateViewServiceComponent>(1);
            world.CreateEntity().Set(new CreateViewServiceComponent(createViewService));
        }
    }
}
