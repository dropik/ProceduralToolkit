using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ZeroColumn : BaseStateDecorator
    {
        public ZeroColumn(IStateBehaviour wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            var result = base.MoveNext(vertex);
            Settings.FSMContext.Column = 0;
            return result;
        }
    }
}
