using System;

namespace ProceduralToolkit.Services.DI
{
    public interface IServiceResolver
    {
        object ResolveService(Type type);
    }
}