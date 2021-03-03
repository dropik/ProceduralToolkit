using System.Collections.Generic;
using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputTwoNewSquares : IStateOutput
    {
        private TilingContext context;

        public OutputTwoNewSquares(TilingContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return vertex + context.XZShift;
            yield return vertex;
            var secondSquare = vertex - context.XZShift;
            yield return new Vector3(secondSquare.x, 0, secondSquare.z);
        }
    }
}