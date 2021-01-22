namespace ProceduralToolkit.Services.DI
{
    public abstract class Service<T> : IService<T>
    {
        private readonly IServiceFactory<T> serviceFactory;
        protected IServiceFactory<T> ServiceFactory => serviceFactory;

        public Service(IServiceFactory<T> serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public abstract T Instance { get; }
        object IService.Instance => Instance;
    }
}