using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class PlaneGenerator : IGenerator
    {
        private readonly float length;
        private readonly float width;

        private const int VERTICES_COUNT = 6;

        public PlaneGenerator(float length, float width)
        {
            this.length = length;
            this.width = width;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var upperLeft = new Vector3(-length / 2, 0, width / 2);
                yield return upperLeft;

                var upperRight = new Vector3(length / 2, 0, width / 2);
                yield return upperRight;

                var lowerLeft = new Vector3(-length / 2, 0, -width / 2);
                yield return lowerLeft;

                yield return lowerLeft;
                yield return upperRight;

                var lowerRight = new Vector3(length / 2, 0, -width / 2);
                yield return lowerRight;
            }
        }

        public IEnumerable<int> Triangles
        {
            get
            {
                for (int i = 0; i < VERTICES_COUNT; i++)
                {
                    yield return i;
                }
            }
        }
    }
}
