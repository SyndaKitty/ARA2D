namespace Core.ServiceRegistration
{
    public class Services
    {
        public readonly ICreateViewService CreateView;

        public Services(ICreateViewService createView)
        {
            CreateView = createView;
        }
    }
}
