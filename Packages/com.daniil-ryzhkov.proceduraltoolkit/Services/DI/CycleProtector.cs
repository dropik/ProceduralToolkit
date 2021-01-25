using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public class CycleProtector : BaseServiceResolverDecorator
    {
        private readonly List<Type> typesStack;

        public CycleProtector(IServiceResolver wrappee) : base(wrappee)
        {
            typesStack = new List<Type>();
        }

        public override object ResolveService(Type type)
        {
            TryPushTypeToStack(type);
            return base.ResolveService(type);
        }

        private void TryPushTypeToStack(Type type)
        {
            if (CycleOccuredOn(type))
            {
                throw new CircularDependencyException();
            }
            typesStack.Add(type);
        }

        private bool CycleOccuredOn(Type type)
        {
            return typesStack.Exists(t => t.IsAssignableFrom(type) || type.IsAssignableFrom(t));
        }
    }
}