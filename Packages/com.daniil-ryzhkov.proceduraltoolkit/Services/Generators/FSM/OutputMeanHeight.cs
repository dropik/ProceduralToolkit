using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputMeanHeight : IStateOutput
    {
        private readonly AdderContext context;

        public OutputMeanHeight(AdderContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            var height = context.Heights.Dequeue();
            var mean = (vertex.y + height) / 4;
            yield return new Vector3(vertex.x, mean, vertex.z);
        }
    }
}
