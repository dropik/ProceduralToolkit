using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputDequeuedWithHeight : IStateOutput
    {
        private readonly InvertorContext context;

        public OutputDequeuedWithHeight(InvertorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            var fromQueue = context.Queue.Dequeue();
            yield return new Vector3(fromQueue.x, fromQueue.y + vertex.y, fromQueue.z);
        }
    }
}
