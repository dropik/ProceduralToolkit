using System;

namespace ProceduralToolkit.Services.DI
{
    public class FuncServiceFactory<T> : IServiceFactory<T>
    {
        private readonly Func<T> serviceProvider;

        public FuncServiceFactory(Func<T> serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T CreateService()
        {
            return serviceProvider.Invoke();
        }
    }
}