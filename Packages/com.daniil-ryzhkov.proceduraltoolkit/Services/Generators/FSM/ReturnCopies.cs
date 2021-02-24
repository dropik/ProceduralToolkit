using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnCopies : BaseStateDecorator
    {
        public ReturnCopies(IState wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            base.MoveNext(vertex);
            yield return vertex;
            foreach (var copy in Settings.FSMContext.RowDuplicatorContext.VerticesCopies)
            {
                yield return copy;
            }
        }
    }
}
