using System;

namespace ProceduralToolkit.Services.ServiceContainer
{
    public interface IServiceResolver
    {
        object ResolveService(Type type);
    }
}