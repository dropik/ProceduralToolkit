using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public static class ServiceContainerFactory
    {
        public static IServiceContainer Create()
        {
            var list = new List<IService>();
            var resolver = new ServiceResolver(list, () => new CycleProtector());
            return new ServiceContainer(list,
                                        resolver,
                                        new ServiceInjector(resolver),
                                        new SingletonServiceFactory(resolver),
                                        new TransientServiceFactory(resolver));
        }
    }
}