using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SkipVertex : BaseReturnVertex
    {
        public SkipVertex(DiamondContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            return null;
        }

        protected override void SetCurrentWithVertex(Vector3 vertex)
        {
            throw new System.NotImplementedException();
        }
    }
}