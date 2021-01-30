namespace ProceduralToolkit.Services.DI
{
    public abstract class ServiceWithFactory<T> : BaseService<T>
    {
        private readonly IServiceFactory<T> serviceFactory;
        protected IServiceFactory<T> ServiceFactory => serviceFactory;

        public ServiceWithFactory(IServiceFactory<T> serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }
    }
}