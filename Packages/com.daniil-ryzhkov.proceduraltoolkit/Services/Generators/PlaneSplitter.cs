using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class PlaneSplitter : BaseGeneratorDecorator
    {
        public PlaneSplitter(IGenerator wrappee, PlaneSplitterSettings settings) : base(wrappee)
        {

        }

        public override IEnumerable<Vector3> Vertices => base.Vertices;

        public override IEnumerable<int> Triangles => base.Triangles;
    }
}