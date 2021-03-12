using System;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] input;
        private readonly int verticesInRow;
        private readonly Vector3 xzShift;
        private readonly Vector3[] output;

        public Diamond(Vector3[] input, int iteration)
        {
            this.input = input;

            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;

            output = new Vector3[2 * verticesInRow * (verticesInRow - 1) + 1];

            xzShift = (input[verticesInRow + 1] - input[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                var index = 0;
                foreach (var vertex in input)
                {
                    HandleVertexFromInput(vertex, index);
                    index++;
                }
                return output;
            }
        }

        private void HandleVertexFromInput(Vector3 vertex, int index)
        {
            var (row, column) = IndexToRowColumn(index);
            HandleOriginal(vertex, OriginalIndex(row, column));
            HandleDiamond(vertex, row, column);
        }

        private (int row, int column) IndexToRowColumn(int index)
        {
            return (index / verticesInRow, index % verticesInRow);
        }

        private void HandleDiamond(Vector3 vertex, int row, int column)
        {
            if (row == 0)
            {
                if (column == 0)
                {
                    HandleLowerRight(vertex, row, column);
                    return;
                }
                if (column < verticesInRow - 1)
                {
                    HandleLowerLeft(vertex, row, column);
                    HandleLowerRight(vertex, row, column);
                    return;
                }

                HandleLowerLeft(vertex, row, column);
                return;
            }

            if (row < verticesInRow - 1)
            {
                if (column == 0)
                {
                    HandleUpperRight(vertex, row, column);
                    HandleLowerRight(vertex, row, column);
                    return;
                }

                if (column < verticesInRow - 1)
                {
                    HandleUpperLeft(vertex, row, column);
                    HandleUpperRight(vertex, row, column);
                    HandleLowerLeft(vertex, row, column);
                    HandleLowerRight(vertex, row, column);
                    return;
                }

                HandleUpperLeft(vertex, row, column);
                HandleLowerLeft(vertex, row, column);
                return;
            }

            if (column == 0)
            {
                HandleUpperRight(vertex, row, column);
                return;
            }

            if (column < verticesInRow - 1)
            {
                HandleUpperLeft(vertex, row, column);
                HandleUpperRight(vertex, row, column);
                return;
            }

            HandleUpperLeft(vertex, row, column);
        }

        private void HandleOriginal(Vector3 vertex, int index)
        {
            output[index] = vertex;
        }

        private void HandleLowerRight(Vector3 vertex, int row, int column)
        {
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleLowerLeft(Vector3 vertex, int row, int column)
        {
            output[LowerDiamond(row) + column - 1].y += vertex.y;
        }

        private void HandleUpperRight(Vector3 vertex, int row, int column)
        {
            output[UpperDiamond(row) + column].y += vertex.y;
        }

        private void HandleUpperLeft(Vector3 vertex, int row, int column)
        {
            output[UpperDiamond(row) + column - 1].y += vertex.y;
            output[UpperDiamond(row) + column - 1].y /= 4;
            output[UpperDiamond(row) + column - 1] += xzShift;
        }

        private int Original(int row) => row * (2 * verticesInRow - 1);
        private int UpperDiamond(int row) => Original(row) - verticesInRow + 1;
        private int LowerDiamond(int row) => Original(row) + verticesInRow;
        private int OriginalIndex(int row, int column) => Original(row) + column;
    }
}