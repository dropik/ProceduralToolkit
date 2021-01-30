using System;

namespace ProceduralToolkit.Services.DI
{
    public abstract class BaseService<T> : IService<T>
    {
        public abstract T Instance { get; }
        object IService.Instance => Instance;
        public Type InstanceType => typeof(T);
    }
}