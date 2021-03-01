using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreHeight : IVertexPreprocessor
    {
        private readonly AdderContext context;

        public StoreHeight(AdderContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            context.Heights[context.Column] = vertex.y;
        }
    }
}
