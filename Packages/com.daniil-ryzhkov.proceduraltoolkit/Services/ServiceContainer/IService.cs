namespace ProceduralToolkit.Services.ServiceContainer
{
    public interface IService
    {
        object Instance { get; }
    }

    public interface IService<T> : IService
    {
        new T Instance { get; }
    }
}