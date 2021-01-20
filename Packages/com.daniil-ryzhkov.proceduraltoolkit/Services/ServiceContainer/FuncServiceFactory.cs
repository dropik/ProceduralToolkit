using System;

namespace ProceduralToolkit.Services.ServiceContainer
{
    public class FuncServiceFactory<T>
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