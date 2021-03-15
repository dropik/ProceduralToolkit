using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SquareDsaStep : IDsaStep
    {
        private readonly Vector3[] vertices;
        private readonly int length;
        private readonly Vector3 gridSize;
        private readonly IDisplacer displacer;

        public SquareDsaStep(LandscapeContext context, IDisplacer displacer)
        {
            vertices = context.Vertices;
            length = context.Length;
            gridSize = context.GridSize;
            this.displacer = displacer;
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
            square.y = GetNeighboursHeightAverage(row, column, context.Step);
            square.y += displacer.GetDisplacement(context.Iteration);
            return square;
        }

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), gridSize);

        private float GetNeighboursHeightAverage(int row, int column, int step)
        {
            return (UpNeighbour(row, column, step) +
                    RightNeighbour(row, column, step) +
                    DownNeighbour(row, column, step) +
                    LeftNeighbour(row, column, step))
                    /
                    4f;
        }

        private float UpNeighbour(int row, int column, int step)
        {
            row = ShiftBackward(row, step);
            return vertices[GetIndex(row, column)].y;
        }

        private float RightNeighbour(int row, int column, int step)
        {
            column = ShiftForward(column, step);
            return vertices[GetIndex(row, column)].y;
        }

        private float DownNeighbour(int row, int column, int step)
        {
            row = ShiftForward(row, step);
            return vertices[GetIndex(row, column)].y;
        }

        private float LeftNeighbour(int row, int column, int step)
        {
            column = ShiftBackward(column, step);
            return vertices[GetIndex(row, column)].y;
        }

        private int ShiftForward(int index, int step)
        {
            index += step;
            if (index > length - 1)
            {
                index -= length - 1;
            }
            return index;
        }

        private int ShiftBackward(int index, int step)
        {
            index -= step;
            if (index < 0)
            {
                index += length - 1;
            }
            return index;
        }

        private int GetIndex(int row, int column)
            => row * length + column;
    }
}
