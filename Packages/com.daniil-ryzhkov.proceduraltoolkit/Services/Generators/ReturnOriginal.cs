using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnOriginal : BaseReturnVertex
    {
        public ReturnOriginal(DiamondContext context) : base(context) { }

        protected override void SetCurrentWithVertex(Vector3 vertex)
        {
            Context.Current = vertex;
        }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            return vertex;
        }
    }
}