using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputCopy : IStateOutput
    {
        private readonly RowDuplicatorContext context;

        public OutputCopy(RowDuplicatorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return context.VerticesCopies[context.Column];
        }
    }
}
