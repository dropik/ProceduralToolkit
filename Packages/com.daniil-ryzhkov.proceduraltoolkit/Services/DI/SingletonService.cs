namespace ProceduralToolkit.Services.DI
{
    public class SingletonService<T> : Service<T>
    {
        private T instance;

        public SingletonService(IServiceFactory<T> serviceFactory) : base(serviceFactory) { }

        public override T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = ServiceFactory.CreateService();
                }
                return instance;
            }
        }
    }
}