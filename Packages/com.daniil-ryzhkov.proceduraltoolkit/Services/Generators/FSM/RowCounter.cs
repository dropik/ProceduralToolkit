using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class RowCounter : IVertexPreprocessor
    {
        private readonly NormalizingContext context;

        public RowCounter(NormalizingContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.Row++;
        }
    }
}
