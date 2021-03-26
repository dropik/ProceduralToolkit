using System;

namespace ProceduralToolkit.Services.DI
{
    public abstract class BaseServiceFactory : IServiceFactory
    {
        private readonly IServiceResolver resolver;

        public BaseServiceFactory(IServiceResolver resolver)
        {
            this.resolver = resolver;
        }

        public IService CreateService<T, TImplementation>() where TImplementation : class, T
        {
            var factory = new ConstructorServiceFactory<T, TImplementation>(resolver);
            return CreateService(factory);
        }

        public IService CreateService<T>(Func<T> provider)
        {
            var factory = new FuncServiceFactory<T>(provider);
            return CreateService(factory);
        }

        public IService CreateService()
        {
            return CreateService<object>(() => new object());
        }

        protected abstract IService CreateService<T>(IServiceFactory<T> factory);
    }
}