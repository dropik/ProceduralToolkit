using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class CalculateXZShift : ReturnDiamond
    {
        public CalculateXZShift(FSMContext context) : base(context) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            var context = Context.DiamondTilingContext;
            if (context.XZShift == Vector3.zero)
            {
                context.XZShift = (context.First - vertex) / 2;
                context.XZShift = new Vector3(context.XZShift.x, 0, context.XZShift.z);
            }
        }
    }
}