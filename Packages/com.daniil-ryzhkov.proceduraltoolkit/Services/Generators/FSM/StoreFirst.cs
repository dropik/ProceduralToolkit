using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreFirst : ReturnOriginal
    {
        public StoreFirst(FSMContext context) : base(context) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            Context.DiamondTilingContext.First = vertex;
        }
    }
}