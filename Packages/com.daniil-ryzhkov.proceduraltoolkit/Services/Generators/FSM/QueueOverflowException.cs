using System;
using System.Runtime.Serialization;

namespace ProceduralToolkit.Services.Generators.FSM
{

    [Serializable]
    public class QueueOverflowException : Exception
    {
        const string MESSAGE = "Queue overflow\n";

        public QueueOverflowException() : base(MESSAGE) { }
        public QueueOverflowException(string message) : base(MESSAGE + message) { }
        public QueueOverflowException(string message, Exception inner) : base(MESSAGE + message, inner) { }
        protected QueueOverflowException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
