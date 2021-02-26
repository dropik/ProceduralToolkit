using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputDequeued : IStateOutput
    {
        private readonly InvertorContext context;

        public OutputDequeued(InvertorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return context.Queue.Dequeue();
        }
    }
}
