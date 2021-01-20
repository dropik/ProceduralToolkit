namespace ProceduralToolkit.Services.ServiceContainer
{
    public class SingletonService<T>
    {
        private readonly IServiceFactory<T> serviceFactory;
        private T instance;

        public SingletonService(IServiceFactory<T> serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = serviceFactory.CreateService();
                }
                return instance;
            }
        }
    }
}