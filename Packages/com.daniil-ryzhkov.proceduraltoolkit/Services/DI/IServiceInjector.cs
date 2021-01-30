namespace ProceduralToolkit.Services.DI
{
    public interface IServiceInjector
    {
        void InjectServicesTo(object target);
    }
}