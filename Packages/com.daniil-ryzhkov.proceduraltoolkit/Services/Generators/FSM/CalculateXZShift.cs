using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class CalculateXZShift : BaseStateDecorator
    {
        public CalculateXZShift(IState wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            var context = Settings.FSMContext.DiamondTilingContext;
            if (context.XZShift == Vector3.zero)
            {
                context.XZShift = (context.First - vertex) / 2;
                context.XZShift = new Vector3(context.XZShift.x, 0, context.XZShift.z);
            }
            return base.MoveNext(vertex);
        }
    }
}