using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class EnqueueVertex : IVertexPreprocessor
    {
        private readonly InvertorContext context;

        public EnqueueVertex(InvertorContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.Queue.Enqueue(vertex);
        }
    }
}
