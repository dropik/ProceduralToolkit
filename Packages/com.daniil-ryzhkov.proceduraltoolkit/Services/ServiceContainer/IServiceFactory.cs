using System;

namespace ProceduralToolkit.Services.ServiceContainer
{
    public interface IServiceFactory<T>
    {
        T CreateService();
    }

    public interface IServiceFactory : IServiceFactory<IService>
    {
        IService CreateService<T, TImplementation>() where TImplementation : class, T;
        IService CreateService<T>(Func<T> provider);
    }
}