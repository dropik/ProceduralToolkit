using UnityEngine;

namespace ProceduralToolkit.Components
{
    public abstract class Service<T> : MonoBehaviour
    {
        public abstract T Instance { get; }

        protected IServiceCreator<T> serviceCreator;

        public void Init(IServiceCreator<T> serviceCreator)
        {
            this.serviceCreator = serviceCreator;
        }

        protected virtual void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }
    }

    public interface IServiceCreator<T>
    {
        T Create();
    }

    public class DefaultServiceCreator<T, TImplementation> : IServiceCreator<T>
        where TImplementation : T, new()
    {
        public T Create() => new TImplementation();
    }

    public class FuncServiceCreator<T> : IServiceCreator<T>
    {
        private readonly System.Func<T> creator;

        public FuncServiceCreator(System.Func<T> creator)
        {
            this.creator = creator;
        }

        public T Create() => creator.Invoke();
    }

    public abstract class TransientService<T> : Service<T>
    {
        public override T Instance => serviceCreator.Create();
    }

    public abstract class SingletonService<T> : Service<T>
    {
        private T instance;
        public override T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = serviceCreator.Create();
                }
                return instance;
            }
        }
    }
}