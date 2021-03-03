using ProceduralToolkit.Models.FSM;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class CalculateXZShift : IVertexPreprocessor
    {
        private readonly TilingContext context;

        public CalculateXZShift(TilingContext context)
        {
            this.context = context;
        }

        public void Process(Vector3 vertex)
        {
            if (context.XZShift == Vector3.zero)
            {
                context.XZShift = (context.First - vertex) / 2;
                context.XZShift = new Vector3(context.XZShift.x, 0, context.XZShift.z);
            }
        }
    }
}