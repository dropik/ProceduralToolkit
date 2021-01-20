namespace ProceduralToolkit.Services.ServiceContainer
{
    public class TransientService<T>
    {
        private readonly IServiceFactory<T> serviceFactory;

        public TransientService(IServiceFactory<T> serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public T Instance => serviceFactory.CreateService();
    }
}