using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnDiamond : BaseStateDecorator
    {
        public ReturnDiamond(IStateBehaviour wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            base.MoveNext(vertex);
            yield return new Vector3(vertex.x, 0, vertex.z) + Settings.FSMContext.DiamondTilingContext.XZShift;
        }
    }
}
