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

            var diamondStep = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            var step = diamondStep / 2;

            var xzShift = (vertices[GetIndex(diamondStep, diamondStep)] - vertices[0]) / 2f;
            xzShift.y = 0;

            for (int row = step; row < length; row += diamondStep)
            {
                for (int column = step; column < length; column += diamondStep)
                {
                    vertices[GetIndex(row, column)] = vertices[GetIndex(row - step, column - step)] + xzShift;

                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row - step, column + step)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row + step, column - step)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetIndex(row + step, column + step)].y;
                    vertices[GetIndex(row, column)].y /= 4f;

                    vertices[GetIndex(row, column)].y += Displacement;
                }
            }
        }

        private int GetIndex(int row, int column) => row * length + column;

        private float Displacement => displacer.GetDisplacement(iteration);
    }
}