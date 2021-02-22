using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class NewStoreCopy : ReturnOriginal
    {
        public NewStoreCopy(FSMSettings settings) : base(settings) { }

        protected override void PreprocessVertex(Vector3 vertex)
        {
            var context = Settings.FSMContext;
            context.RowDuplicatorContext.VerticesCopies[context.Column] = vertex;
        }
    }
}
