using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly IDisplacer displacer;

        private int iteration;
        private int length;

        public Diamond(IDisplacer displacer)
        {
            this.displacer = displacer;
        }

        public void CalculateDiamonds(Vector3[] vertices, int iteration)
        {
            this.iteration = iteration;
            length = (int)Mathf.Sqrt(vertices.Length);

            var xzShift = (vertices[GetIndex(2, 2)] - vertices[0]) / 2f;
            xzShift.y = 0;

            for (int i = 1; i < length; i += 2)
            {
                for (int j = 1; j < length; j += 2)
                {
                    vertices[GetIndex(i, j)] = vertices[GetIndex(i - 1, j - 1)] + xzShift;

                    vertices[GetIndex(i, j)].y += vertices[GetIndex(i - 1, j + 1)].y;
                    vertices[GetIndex(i, j)].y += vertices[GetIndex(i + 1, j - 1)].y;
                    vertices[GetIndex(i, j)].y += vertices[GetIndex(i + 1, j + 1)].y;
                    vertices[GetIndex(i, j)].y /= 4f;

                    vertices[GetIndex(i, j)].y += Displacement;
                }
            }
        }

        private int GetIndex(int row, int column) => row * length + column;

        private float Displacement => displacer.GetDisplacement(iteration);
    }
}