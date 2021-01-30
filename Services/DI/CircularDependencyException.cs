using System;

namespace ProceduralToolkit.Services.DI
{
    [Serializable]
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException() : base("Circularly dependent services") { }
        public CircularDependencyException(string message) : base(message) { }
        public CircularDependencyException(string message, System.Exception inner) : base(message, inner) { }
        protected CircularDependencyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}