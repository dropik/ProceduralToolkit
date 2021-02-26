using System;
using System.Runtime.Serialization;

namespace ProceduralToolkit.Services.Generators.FSM
{

    [Serializable]
    public class QueueUnderflowException : Exception
    {
        const string MESSAGE = "Queue underflow\n";

        public QueueUnderflowException() : base(MESSAGE) { }
        public QueueUnderflowException(string message) : base(MESSAGE + message) { }
        public QueueUnderflowException(string message, Exception inner) : base(MESSAGE + message, inner) { }
        protected QueueUnderflowException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
