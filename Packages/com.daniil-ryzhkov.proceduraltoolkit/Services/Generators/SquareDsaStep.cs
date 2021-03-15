using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SquareDsaStep : IDsaStep
    {
        private readonly Vector3[] vertices;
        private readonly IDisplacer displacer;
        private readonly int length;

        public SquareDsaStep(Vector3[] vertices, IDisplacer displacer)
        {
            this.vertices = vertices;
            this.displacer = displacer;
            length = (int)Mathf.Sqrt(vertices.Length);
        }

        public void Execute(int iteration)
        {
            var squareStep = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            var step = squareStep / 2;
            var start = step;

            Vector3 GetShift(int row, int column) => Vector3.Scale(new Vector3(column, 0, row) / (length - 1), vertices[length * length - 1] - vertices[0]);
            Vector3 GetVertexOnGrid(int row, int column) => Vector3.Scale(new Vector3(1, 0, 1), vertices[0] + GetShift(row, column));

            for (int row = 0; row < length; row += step)
            {
                for (int column = start; column < length; column += squareStep)
                {
                    vertices[GetIndex(row, column)] = GetVertexOnGrid(row, column);

                    vertices[GetIndex(row, column)].y += vertices[GetCircularIndex(row - step, column)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetCircularIndex(row, column + step)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetCircularIndex(row + step, column)].y;
                    vertices[GetIndex(row, column)].y += vertices[GetCircularIndex(row, column - step)].y;
                    vertices[GetIndex(row, column)].y /= 4f;

                    vertices[GetIndex(row, column)].y += displacer.GetDisplacement(iteration);
                }
                start = (start + step) % squareStep;
            }
        }

        private int GetIndex(int row, int column) => row * length + column;
        
        private int GetCircularIndex(int row, int column)
        {
            if (row < 0)
            {
                row += length - 1;
            }
            else if (row > length - 1)
            {
                row -= length - 1;
            }

            if (column < 0)
            {
                column += length - 1;
            }
            else if (column > length - 1)
            {
                column -= length - 1;
            }

            return GetIndex(row, column);
        }
    }
}
