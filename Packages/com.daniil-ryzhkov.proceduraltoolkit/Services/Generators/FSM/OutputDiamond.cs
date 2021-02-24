using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputDiamond : IStateOutput
    {
        private readonly DiamondTilingContext context;

        public OutputDiamond(DiamondTilingContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return new Vector3(vertex.x, 0, vertex.z) + context.XZShift;
        }
    }
}
