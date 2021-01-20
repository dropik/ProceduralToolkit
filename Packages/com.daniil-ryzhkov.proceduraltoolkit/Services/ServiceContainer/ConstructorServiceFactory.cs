using System;
using System.Reflection;

namespace ProceduralToolkit.Services.ServiceContainer
{
    public class ConstructorServiceFactory<T, TImplementation> : IServiceFactory<T>
        where TImplementation : class, T
    {
        private readonly Type implementationType;
        private readonly IServiceContainer services;

        public ConstructorServiceFactory(IServiceContainer services)
        {
            implementationType = typeof(TImplementation);
            this.services = services;
        }

        public T CreateService()
        {
            var constructor = implementationType.GetConstructors()[0];
            var parametersInfo = constructor.GetParameters();
            var parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                var settings = services.GetService(parametersInfo[i].ParameterType);
                parameters[i] = settings;
            }
            var newService = constructor.Invoke(parameters);
            return (T)newService;
        }
    }
}