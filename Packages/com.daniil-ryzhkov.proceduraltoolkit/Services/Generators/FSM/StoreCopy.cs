using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreCopy : IVertexPreprocessor
    {
        private readonly RowDuplicatorContext context;

        public StoreCopy(RowDuplicatorContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.VerticesCopies[context.Column] = vertex;
        }
    }
}
