namespace ProceduralToolkit.Services.DI
{
    public class TransientServiceFactory : BaseServiceFactory
    {
        public TransientServiceFactory(IServiceResolver resolver) : base(resolver) { }

        protected override IService CreateService<T>(IServiceFactory<T> factory)
        {
            return new TransientService<T>(factory);
        }
    }
}