using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
    public class PlaneGenerator
    {
        private Vector3 center;
        private float length;
        private float width;

        public PlaneGenerator(Vector3 center, float length, float width)
        {
            this.center = center;
            this.length = length;
            this.width = width;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var upperLeft = center + new Vector3(-length / 2, 0, width / 2);
                yield return upperLeft;

                var upperRight = center + new Vector3(length / 2, 0, width / 2);
                yield return upperRight;

                var lowerLeft = center + new Vector3(-length / 2, 0, -width / 2);
                yield return lowerLeft;

                yield return lowerLeft;
                yield return upperRight;

                var lowerRight = center + new Vector3(length / 2, 0, -width / 2);
                yield return lowerRight;
            }
        }

        public IEnumerable<int> Triangles
        {
            get
            {
                var verticesCount = 6;
                for (int i = 0; i < verticesCount; i++)
                {
                    yield return i;
                }
            }
        }
    }
}
