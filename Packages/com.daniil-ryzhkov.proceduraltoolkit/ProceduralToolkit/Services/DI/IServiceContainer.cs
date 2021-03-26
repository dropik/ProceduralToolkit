using System;

namespace ProceduralToolkit.Services.DI
{
    public interface IServiceContainer
    {
        T GetService<T>();
        object GetService(Type type);

        void InjectServicesTo(object target);

        void AddSingleton<TImplementation>() where TImplementation : class;
        void AddSingleton<T, TImplementation>() where TImplementation : class, T;
        void AddSingleton<T>(T instance);
        void AddSingleton<T>(Func<T> provider);
        
        void AddTransient<TImplementation>() where TImplementation : class;
        void AddTransient<T, TImplementation>() where TImplementation : class, T;
        void AddTransient<T>(Func<T> provider);
    }
}