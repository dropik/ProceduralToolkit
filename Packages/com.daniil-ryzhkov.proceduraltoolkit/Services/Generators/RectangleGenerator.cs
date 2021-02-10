using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class RectangleGenerator
    {
        private readonly float length;
        private readonly float width;

        public RectangleGenerator(RectangleGeneratorSettings settings)
        {
            this.length = settings.Length;
            this.width = settings.Width;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                yield return new Vector3(-length / 2, 0, width / 2);
                yield return new Vector3(length / 2, 0, width / 2);
                yield return new Vector3(-length / 2, 0, -width / 2);
                yield return new Vector3(length / 2, 0, -width / 2);
            }
        }

        public IEnumerable<Square> Squares => new Square[]
        {
            new Square(0, 1, 3, 2)
        };
    }
}
