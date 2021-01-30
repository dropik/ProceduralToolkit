namespace ProceduralToolkit.Services.DI
{
    public class TransientService<T> : ServiceWithFactory<T>
    {
        public TransientService(IServiceFactory<T> serviceFactory) : base(serviceFactory) { }

        public override T Instance => ServiceFactory.CreateService();
    }
}