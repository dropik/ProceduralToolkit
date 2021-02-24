using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreCopy : IVertexPreprocessor
    {
        private readonly FSMContext context;

        public StoreCopy(FSMContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.RowDuplicatorContext.VerticesCopies[context.Column] = vertex;
        }
    }
}
