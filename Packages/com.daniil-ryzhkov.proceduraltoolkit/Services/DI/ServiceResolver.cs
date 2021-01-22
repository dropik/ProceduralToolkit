using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceResolver : IServiceResolver
    {
        private readonly IList<IService> services;

        public ServiceResolver(IList<IService> services)
        {
            this.services = services;
        }

        public object ResolveService(Type type)
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