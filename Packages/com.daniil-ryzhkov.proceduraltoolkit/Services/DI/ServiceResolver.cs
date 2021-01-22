using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceResolver : IServiceResolver
    {
        private readonly IList<IService> services;
        private readonly Func<ICycleProtector> protectorProvider;

        public ServiceResolver(IList<IService> services, Func<ICycleProtector> protectorProvider)
        {
            this.services = services;
            this.protectorProvider = protectorProvider;
        }

        public object ResolveService(Type type)
        {
            var protector = protectorProvider.Invoke();
            return ResolveService(type, protector);
        }

        private object ResolveService(Type type, ICycleProtector protector)
        {
            protector.Push(type);
            return FindService(type);
        }

        private object FindService(Type type)
        {
            foreach (var service in services)
            {
                if (type.IsAssignableFrom(service.InstanceType))
                {
                    return service.Instance;
                }
            }
            throw new NotRegisteredServiceException();
        }
    }
}