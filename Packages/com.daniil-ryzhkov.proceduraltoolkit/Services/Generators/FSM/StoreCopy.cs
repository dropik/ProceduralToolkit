using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreCopy : BaseStateDecorator
    {
        public StoreCopy(IStateBehaviour wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            var context = Settings.FSMContext;
            context.RowDuplicatorContext.VerticesCopies[context.Column] = vertex;
            return base.MoveNext(vertex);
        }
    }
}
