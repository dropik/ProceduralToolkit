using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnCopies : BaseState
    {
        public ReturnCopies(FSMSettings settings) : base(settings) { }

        protected override IEnumerable<Vector3> GetResultVertices(Vector3 vertex)
        {
            yield return vertex;
            foreach (var copy in Settings.FSMContext.RowDuplicatorContext.VerticesCopies)
            {
                yield return copy;
            }
        }
    }
}
