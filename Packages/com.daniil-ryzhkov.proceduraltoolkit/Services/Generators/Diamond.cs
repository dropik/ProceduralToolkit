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

            var step = (int)((length - 1) / Mathf.Pow(2, iteration - 1));

            var xzShift = (vertices[GetIndex(step, step)] - vertices[0]) / 2f;
            xzShift.y = 0;

            for (int row = step / 2; row < length; row += step)
            {
                for (int column = step / 2; column < length; column += step)
                {
                    vertices[GetIndex(row, column)] = vertices[GetIndex(row - 1, column - 1)] + xzShift;

                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row - 1, column + 1)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row + 1, column - 1)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row + 1, column + 1)].y;
                    vertices[GetIndex(row, column)].y /= 4f;

                    vertices[GetIndex(row, column)].y += Displacement;
                }
            }
        }

        private int GetIndex(int row, int column) => row * length + column;

        private float Displacement => displacer.GetDisplacement(iteration);
    }
}