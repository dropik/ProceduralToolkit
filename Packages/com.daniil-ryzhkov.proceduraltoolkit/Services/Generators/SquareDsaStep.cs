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
                    vertices[row * length + column] = GetVertexOnGrid(row, column);

                    var upRow = row - step;
                    if (upRow < 0)
                    {
                        upRow += length - 1;
                    }
                    vertices[row * length + column].y += vertices[upRow * length + column].y;

                    var rightColumn = column + step;
                    if (rightColumn > length - 1)
                    {
                        rightColumn -= length - 1;
                    }
                    vertices[row * length + column].y += vertices[row * length + rightColumn].y;

                    var downRow = row + step;
                    if (downRow > length - 1)
                    {
                        downRow -= length - 1;
                    }
                    vertices[row * length + column].y += vertices[downRow * length + column].y;

                    var leftColumn = column - step;
                    if (leftColumn < 0)
                    {
                        leftColumn += length - 1;
                    }
                    vertices[row * length + column].y += vertices[row * length + leftColumn].y;

                    vertices[row * length + column].y /= 4f;

                    vertices[row * length + column].y += displacer.GetDisplacement(iteration);
                }
                start = (start + step) % squareStep;
            }
        }
    }
}
