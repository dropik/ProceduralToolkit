using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class CalculateXZShift : ReturnDiamond
    {
        public CalculateXZShift(DiamondTilingContext context) : base(context) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            if (Context.XZShift == Vector3.zero)
            {
                Context.XZShift = (Context.First - vertex) / 2;
            }
        }
    }
}