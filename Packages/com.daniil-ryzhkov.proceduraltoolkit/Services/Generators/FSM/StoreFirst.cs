using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreFirst : ReturnOriginal
    {
        public StoreFirst(DiamondTilingContext context) : base(context) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            Context.First = vertex;
        }
    }
}