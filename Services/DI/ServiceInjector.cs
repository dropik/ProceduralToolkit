using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ProceduralToolkit.Services.DI
{
    public class ServiceInjector : IServiceInjector
    {
        private readonly IServiceResolver resolver;
        private object target;

        public ServiceInjector(IServiceResolver resolver)
        {
            this.resolver = resolver;
        }

        public void InjectServicesTo(object target)
        {
            this.target = target;
            foreach (var field in ServiceFields)
            {
                var service = resolver.ResolveService(field.FieldType);
                field.SetValue(target, service);
            }
        }

        private IEnumerable<FieldInfo> ServiceFields =>
            from type in TargetTypes
            from field in GetPrivateFieldsInType(type)
            where field.IsDefined(typeof(ServiceAttribute), false)
            select field;
        
        private IEnumerable<Type> TargetTypes
        {
            get
            {
                var type = target.GetType();
                while(DidNotReachFinishType(type))
                {
                    yield return type;
                    type = type.BaseType;
                }
            }
        }

        private bool DidNotReachFinishType(Type type) =>
            !FinishTypes.Contains(type);

        private IEnumerable<Type> FinishTypes
        {
            get
            {
                yield return typeof(MonoBehaviour);
                yield return typeof(Component);
                yield return typeof(UnityEngine.Object);
                yield return typeof(object);
            }
        }

        private IEnumerable<FieldInfo> GetPrivateFieldsInType(Type type) =>
            type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
    }
}