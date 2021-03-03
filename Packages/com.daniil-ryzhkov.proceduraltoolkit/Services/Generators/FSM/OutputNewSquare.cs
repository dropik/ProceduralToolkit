using System.Collections.Generic;
using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputNewSquare : IStateOutput
    {
        private readonly TilingContext context;

        public OutputNewSquare(TilingContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return vertex + context.XZShift;
            yield return vertex;
        }
    }
}