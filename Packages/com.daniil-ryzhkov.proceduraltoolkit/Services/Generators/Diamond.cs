using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond : IDsaStep
    {
        private readonly IDisplacer displacer;

        public Diamond(IDisplacer displacer)
        {
            this.displacer = displacer;
        }

        public void Execute(Vector3[] vertices, int iteration)
        {
            var length = GetGridSideLength(vertices);
            var diamondStep = GetDiamondStep(iteration, length);
            var step = GetStep(diamondStep);
            var xzShift = GetXzShift(vertices, length, diamondStep);

            CalculateDiamonds(vertices, length, iteration, diamondStep, step, xzShift);
        }

        private int GetGridSideLength(Vector3[] vertices) => (int)Mathf.Sqrt(vertices.Length);

        private int GetDiamondStep(int iteration, int length) => (int)((length - 1) / Mathf.Pow(2, iteration - 1));

        private int GetStep(int diamondStep) => diamondStep / 2;

        private Vector3 GetXzShift(Vector3[] vertices, int length, int diamondStep)
        {
            var xzShift = (vertices[GetIndex(length, diamondStep, diamondStep)] - vertices[0]) / 2f;
            xzShift.y = 0;
            return xzShift;
        }

        private void CalculateDiamonds(Vector3[] vertices, int length, int iteration, int diamondStep, int step, Vector3 xzShift)
        {
            for (int row = step; row < length; row += diamondStep)
            {
                for (int column = step; column < length; column += diamondStep)
                {
                    CalculateOneDiamond(vertices, length, iteration, row, column, step, xzShift);
                }
            }
        }

        private void CalculateOneDiamond(Vector3[] vertices, int length, int iteration, int row, int column, int step, Vector3 xzShift)
        {
            vertices[GetIndex(length, row, column)] = vertices[GetIndex(length, row - step, column - step)] + xzShift;

            vertices[GetIndex(length, row, column)].y += vertices[GetIndex(length, row - step, column + step)].y;
            vertices[GetIndex(length, row, column)].y += vertices[GetIndex(length, row + step, column - step)].y;
            vertices[GetIndex(length, row, column)].y += vertices[GetIndex(length, row + step, column + step)].y;
            vertices[GetIndex(length, row, column)].y /= 4f;

            vertices[GetIndex(length, row, column)].y += Displacement(iteration);
        }

        private int GetIndex(int length, int row, int column) => row * length + column;

        private float Displacement(int iteration) => displacer.GetDisplacement(iteration);
    }
}