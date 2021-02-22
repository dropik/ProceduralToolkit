using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ResetAndReturnNext : ReturnNext
    {
        public ResetAndReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            FSMContext context)
        : base(inputVerticesEnumerator, context) { }

        protected override void PreMoveNext()
        {
            if (Context.Column == 0)
            {
                InputVerticesEnumerator.Reset();
            }
        }
    }
}
