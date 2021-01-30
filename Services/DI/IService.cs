using System;

namespace ProceduralToolkit.Services.DI
{
    public interface IService
    {
        object Instance { get; }
        Type InstanceType { get; }
    }

    public interface IService<T> : IService
    {
        new T Instance { get; }
    }
}