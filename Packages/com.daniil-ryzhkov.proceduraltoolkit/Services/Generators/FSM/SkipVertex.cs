using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class SkipVertex : BaseStateDecorator
    {
        public SkipVertex(IState wrappee, FSMSettings settings) : base(wrappee, settings) { }

        public override IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            base.MoveNext(vertex);
            yield break;
        }
    }
}