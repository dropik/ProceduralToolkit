using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseGeneratorDecorator : IGenerator
    {
        private readonly IGenerator wrappee;

        public BaseGeneratorDecorator(IGenerator wrappee)
        {
            this.wrappee = wrappee;
        }

        public virtual IEnumerable<Vector3> Vertices => wrappee.Vertices;
        public virtual IEnumerable<int> Triangles => wrappee.Triangles;
    }
}