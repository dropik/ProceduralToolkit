namespace ProceduralToolkit.Services.DI
{
    public class SingletonServiceFactory : BaseServiceFactory
    {
        public SingletonServiceFactory(IServiceResolver resolver) : base(resolver) { }

        protected override IService CreateService<T>(IServiceFactory<T> factory)
        {
            return new SingletonService<T>(factory);
        }
    }
}