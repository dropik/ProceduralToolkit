using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreFirst : ReturnOriginal
    {
        public StoreFirst(FSMSettings settings) : base(settings) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            Settings.FSMContext.DiamondTilingContext.First = vertex;
        }
    }
}