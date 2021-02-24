using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputCopies : IStateOutput
    {
        private RowDuplicatorContext context;

        public OutputCopies(RowDuplicatorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return vertex;
            foreach (var copy in context.VerticesCopies)
            {
                yield return copy;
            }
        }
    }
}
