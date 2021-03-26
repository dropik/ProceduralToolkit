using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public static class ServiceContainerFactory
    {
        public static IServiceContainer Create()
        {
            var list = new List<IService>();
            var baseResolver = new ServiceResolver(list);
            var resolver = new ServiceResolverProxy(() => new CycleProtector(baseResolver));
            return new ServiceContainer(list,
                                        resolver,
                                        new ServiceInjector(resolver),
                                        new SingletonServiceFactory(resolver),
                                        new TransientServiceFactory(resolver));
        }
    }
}