using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreFirst : IVertexPreprocessor
    {
        private readonly TilingContext context;

        public StoreFirst(TilingContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.First = vertex;
        }
    }
}