using System;
using System.Collections.Generic;
using System.Reflection;

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
                var instance = service.Instance;
                if (type.IsAssignableFrom(instance.GetType()))
                {
                    return instance;
                }
            }
            throw new NotRegisteredServiceException();
        }
    }
}