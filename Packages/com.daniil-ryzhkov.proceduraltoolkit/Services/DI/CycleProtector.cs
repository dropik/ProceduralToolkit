using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    public class CycleProtector : ICycleProtector
    {
        private readonly List<Type> typesStack;

        public CycleProtector()
        {
            typesStack = new List<Type>();
        }

        public void Push(Type type)
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