using System;

namespace ProceduralToolkit.Services.DI
{
    public class ConstructorServiceFactory<T, TImplementation> : IServiceFactory<T>
        where TImplementation : class, T
    {
        private readonly Type implementationType;
        private readonly IServiceResolver resolver;

        public ConstructorServiceFactory(IServiceResolver resolver)
        {
            implementationType = typeof(TImplementation);
            this.resolver = resolver;
        }

        public T CreateService()
        {
            var constructor = implementationType.GetConstructors()[0];
            var parametersInfo = constructor.GetParameters();
            var parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                var settings = resolver.ResolveService(parametersInfo[i].ParameterType);
                parameters[i] = settings;
            }
            var newService = constructor.Invoke(parameters);
            return (T)newService;
        }
    }
}