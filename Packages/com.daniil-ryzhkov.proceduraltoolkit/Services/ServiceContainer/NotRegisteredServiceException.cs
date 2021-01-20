using System;

namespace ProceduralToolkit.Services.ServiceContainer
{
    [Serializable]
    public class NotRegisteredServiceException : Exception
    {
        public NotRegisteredServiceException() { }
        public NotRegisteredServiceException(string message) : base(message) { }
        public NotRegisteredServiceException(string message, Exception inner) : base(message, inner) { }
        protected NotRegisteredServiceException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}