using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StoreFirst : BaseStateDecorator
    {
        public StoreFirst(IStateBehaviour wrappee,FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            Settings.FSMContext.DiamondTilingContext.First = vertex;
            return base.MoveNext(vertex);
        }
    }
}