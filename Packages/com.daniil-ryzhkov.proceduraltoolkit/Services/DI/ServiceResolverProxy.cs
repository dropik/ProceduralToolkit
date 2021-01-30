using System;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceResolverProxy : IServiceResolver
    {
        private readonly Func<IServiceResolver> resolverProvider;

        public ServiceResolverProxy(Func<IServiceResolver> resolverProvider)
        {
            this.resolverProvider = resolverProvider;
        }

        public object ResolveService(Type type)
        {
            var resolver = resolverProvider.Invoke();
            return resolver.ResolveService(type);
        }
    }
}