using System;

namespace ProceduralToolkit.Services.ServiceContainer
{
    public interface IServiceContainer
    {
        object GetService(Type type);
    }
}