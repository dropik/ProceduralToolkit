using ProceduralToolkit.Models;
using System.Collections.Generic;
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
            var context = new SquareContext(length, iteration);
            CalculateSquares(context);
        }

        private void CalculateSquares(SquareContext context)
        {
            foreach (var (row, column) in GetRowsAndColumnsOfSquares(context))
            {
                vertices[GetIndex(row, column)] = GetSquare(row, column, context);
            }
        }

        private IEnumerable<(int row, int column)> GetRowsAndColumnsOfSquares(SquareContext context)
        {
            var start = context.Step;
            for (int row = 0; row < length; row += context.Step)
            {
                for (int column = start; column < length; column += context.SquareStep)
                {
                    yield return (row, column);
                }
                start = (start + context.Step) % context.SquareStep;
            }
        }

        private Vector3 GetSquare(int row, int column, SquareContext context)
        {
            var square = GetVertexOnGrid(row, column);
            square = GetNeighboursAverage(square, row, column, context.Step);
            square.y += displacer.GetDisplacement(context.Iteration);
            return square;
        }

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row) / (length - 1), vertices[length * length - 1] - vertices[0]);

        private Vector3 GetNeighboursAverage(Vector3 square, int row, int column, int step)
        {
            square.y += vertices[GetCircularIndex(row - step, column)].y;
            square.y += vertices[GetCircularIndex(row, column + step)].y;
            square.y += vertices[GetCircularIndex(row + step, column)].y;
            square.y += vertices[GetCircularIndex(row, column - step)].y;
            square.y /= 4f;

            return square;
        }

        private int GetIndex(int row, int column)
            => row * length + column;
        
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
