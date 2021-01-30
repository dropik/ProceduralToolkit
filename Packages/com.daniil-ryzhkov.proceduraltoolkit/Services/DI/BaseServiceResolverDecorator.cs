using System;

namespace ProceduralToolkit.Services.DI
{
    public abstract class BaseServiceResolverDecorator : IServiceResolver
    {
        private readonly IServiceResolver wrappee;

        public BaseServiceResolverDecorator(IServiceResolver wrappee)
        {
            this.wrappee = wrappee;
        }

        public virtual object ResolveService(Type type)
        {
            return wrappee.ResolveService(type);
        }
    }
}