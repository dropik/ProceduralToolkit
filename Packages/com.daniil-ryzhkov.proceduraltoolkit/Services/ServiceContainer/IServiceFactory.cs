namespace ProceduralToolkit.Services.ServiceContainer
{
    public interface IServiceFactory<T>
    {
        T CreateService();
    }
}