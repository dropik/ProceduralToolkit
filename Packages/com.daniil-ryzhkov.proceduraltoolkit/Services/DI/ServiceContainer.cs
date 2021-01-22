using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly IList<IService> list;
        private readonly IServiceResolver resolver;
        private readonly IServiceInjector injector;
        private readonly IServiceFactory singletonFactory;
        private readonly IServiceFactory transientFactory;

        public ServiceContainer(
            IList<IService> list,
            IServiceResolver resolver,
            IServiceInjector injector,
            IServiceFactory singletonFactory,
            IServiceFactory transientFactory)
        {
            this.list = list;
            this.resolver = resolver;
            this.injector = injector;
            this.singletonFactory = singletonFactory;
            this.transientFactory = transientFactory;
        }

        public static IServiceContainer Create()
        {
            var list = new List<IService>();
            var resolver = new ServiceResolver(list);
            return new ServiceContainer(list,
                                        resolver,
                                        new ServiceInjector(resolver),
                                        new SingletonServiceFactory(resolver),
                                        new TransientServiceFactory(resolver));
        }

        public void AddSingleton<TImplementation>() where TImplementation : class
        {
            AddSingleton<TImplementation, TImplementation>();
        }

        public void AddSingleton<T, TImplementation>() where TImplementation : class, T
        {
            AddService<T, TImplementation>(singletonFactory);
        }

        public void AddSingleton<T>(T instance)
        {
            AddSingleton<T>(() => instance);
        }

        public void AddSingleton<T>(Func<T> provider)
        {
            AddService<T>(singletonFactory, provider);
        }

        public void AddTransient<TImplementation>() where TImplementation : class
        {
            AddTransient<TImplementation, TImplementation>();
        }

        public void AddTransient<T, TImplementation>() where TImplementation : class, T
        {
            AddService<T, TImplementation>(transientFactory);
        }

        public void AddTransient<T>(Func<T> provider)
        {
            AddService<T>(transientFactory, provider);
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type)
        {
            return resolver.ResolveService(type);
        }

        public void InjectServicesTo(object target)
        {
            injector.InjectServicesTo(target);
        }

        private void AddService<T, TImplementation>(IServiceFactory factory)
            where TImplementation : class, T
        {
            var service = factory.CreateService<T, TImplementation>();
            list.Add(service);
        }

        private void AddService<T>(IServiceFactory factory, Func<T> provider)
        {
            var service = factory.CreateService<T>(provider);
            list.Add(service);
        }
    }
}