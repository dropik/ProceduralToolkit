using System.Reflection;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceInjector : IServiceInjector
    {
        private readonly IServiceResolver resolver;

        public ServiceInjector(IServiceResolver resolver)
        {
            this.resolver = resolver;
        }

        public void InjectServicesTo(object target)
        {
            var type = target.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(typeof(ServiceAttribute), false);
                if (attributes.Length > 0)
                {
                    var service = resolver.ResolveService(field.FieldType);
                    field.SetValue(target, service);
                }
            }
        }
    }
}