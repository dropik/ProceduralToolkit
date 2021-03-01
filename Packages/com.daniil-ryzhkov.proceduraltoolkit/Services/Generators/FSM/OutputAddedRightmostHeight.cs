using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputAddedRightmostHeight : IStateOutput
    {
        private readonly AdderContext context;

        public OutputAddedRightmostHeight(AdderContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return new Vector3(vertex.x, vertex.y + context.Heights[context.Column + 1], vertex.z);
        }
    }
}
