namespace Core.ServiceRegistration
{
    public struct CreateViewServiceComponent
    {
        public ICreateViewService Instance;

        public CreateViewServiceComponent(ICreateViewService instance)
        {
            Instance = instance;
        }
    }
}
