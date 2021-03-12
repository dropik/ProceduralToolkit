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
                for (int i = 1; i < 2 * verticesInRow - 1; i += 2)
                {
                    for (int j = 0; j < verticesInRow - 1; j++)
                    {
                        HandleOriginal(input[i / 2 * verticesInRow + j], OriginalIndex(i / 2, j));
                        HandleOriginal(input[i / 2 * verticesInRow + j + 1], OriginalIndex(i / 2, j + 1));
                        HandleOriginal(input[(i + 1) / 2 * verticesInRow + j], OriginalIndex((i + 1) / 2, j));
                        HandleOriginal(input[(i + 1) / 2 * verticesInRow + j + 1], OriginalIndex((i + 1) / 2, j + 1));
                    }
                }

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
                    HandleLowerRight(vertex, LowerRightIndex(row, column));
                    return;
                }
                if (column < verticesInRow - 1)
                {
                    HandleLowerLeft(vertex, LowerLeftIndex(row, column));
                    HandleLowerRight(vertex, LowerRightIndex(row, column));
                    return;
                }

                HandleLowerLeft(vertex, LowerLeftIndex(row, column));
                return;
            }

            if (row < verticesInRow - 1)
            {
                if (column == 0)
                {
                    HandleUpperRight(vertex, UpperRightIndex(row, column));
                    HandleLowerRight(vertex, LowerRightIndex(row, column));
                    return;
                }

                if (column < verticesInRow - 1)
                {
                    HandleUpperLeft(vertex, UpperLeftIndex(row, column));
                    HandleUpperRight(vertex, UpperRightIndex(row, column));
                    HandleLowerLeft(vertex, LowerLeftIndex(row, column));
                    HandleLowerRight(vertex, LowerRightIndex(row, column));
                    return;
                }

                HandleUpperLeft(vertex, UpperLeftIndex(row, column));
                HandleLowerLeft(vertex, LowerLeftIndex(row, column));
                return;
            }

            if (column == 0)
            {
                HandleUpperRight(vertex, UpperRightIndex(row, column));
                return;
            }

            if (column < verticesInRow - 1)
            {
                HandleUpperLeft(vertex, UpperLeftIndex(row, column));
                HandleUpperRight(vertex, UpperRightIndex(row, column));
                return;
            }

            HandleUpperLeft(vertex, UpperLeftIndex(row, column));
        }

        private void HandleOriginal(Vector3 vertex, int index)
        {
            output[index] = vertex;
        }

        private void HandleLowerRight(Vector3 vertex, int index)
        {
            output[index] = vertex;
        }

        private void HandleLowerLeft(Vector3 vertex, int index)
        {
            output[index].y += vertex.y;
        }

        private void HandleUpperRight(Vector3 vertex, int index)
        {
            output[index].y += vertex.y;
        }

        private void HandleUpperLeft(Vector3 vertex, int index)
        {
            output[index].y += vertex.y;
            output[index].y /= 4;
            output[index] += xzShift;
        }

        private int Original(int row) => row * (2 * verticesInRow - 1);
        private int UpperDiamond(int row) => Original(row) - verticesInRow + 1;
        private int LowerDiamond(int row) => Original(row) + verticesInRow;
        private int OriginalIndex(int row, int column) => Original(row) + column;
        private int LowerRightIndex(int row, int column) => LowerDiamond(row) + column;
        private int LowerLeftIndex(int row, int column) => LowerDiamond(row) + column - 1;
        private int UpperRightIndex(int row, int column) => UpperDiamond(row) + column;
        private int UpperLeftIndex(int row, int column) => UpperDiamond(row) + column - 1;
    }
}