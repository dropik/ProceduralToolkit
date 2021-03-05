using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputNormalized : IStateOutput
    {
        private readonly int count;

        public OutputNormalized(int count)
        {
            this.count = count;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return new Vector3(vertex.x, vertex.y / count, vertex.z);
        }
    }
}
