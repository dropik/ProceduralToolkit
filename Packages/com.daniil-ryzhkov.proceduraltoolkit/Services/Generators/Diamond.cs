using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] inputVertices;
        private readonly int verticesInRow;
        private readonly int rowStep;
        private readonly int upperDiamondShift;
        private readonly int lowerDiamondShift;
        private readonly Vector3 xzShift;
        private readonly Vector3[] output;

        public Diamond(Vector3[] inputVertices, int iteration)
        {
            this.inputVertices = inputVertices;
            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;
            rowStep = 2 * verticesInRow - 1;
            upperDiamondShift = -verticesInRow + 1;
            lowerDiamondShift = verticesInRow;
            output = new Vector3[2 * verticesInRow * (verticesInRow - 1) + 1];
            xzShift = (inputVertices[verticesInRow + 1] - inputVertices[0]) / 2f;
            xzShift.y = 0;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var index = 0;
                foreach (var vertex in inputVertices)
                {
                    GenerateVerticesFor(vertex, index);
                    index++;
                }
                return output;
            }
        }

        private void GenerateVerticesFor(Vector3 vertex, int index)
        {
            var row = index / verticesInRow;
            var column = index % verticesInRow;

            HandleOriginal(vertex, row, column);

            Action<Vector3, int, int> diamondHandler = HandleLowerRight;
            diamondHandler += HandleLowerLeft;

            if (row == verticesInRow - 1)
            {
                diamondHandler -= HandleLowerLeft;
                diamondHandler -= HandleLowerRight;
            }

            if (column == 0)
            {
                diamondHandler -= HandleLowerLeft;
            }

            if (column == verticesInRow - 1)
            {
                diamondHandler -= HandleLowerRight;
            }

            diamondHandler?.Invoke(vertex, row, column);

            if (row == 0)
            {
                HandleFirstRow(vertex, column);
                return;
            }

            HandleNonFirstRow(vertex, row, column);
        }

        private void HandleNonFirstRow(Vector3 vertex, int row, int column)
        {
            if (row < verticesInRow - 1)
            {
                HandleMiddleRow(vertex, row, column);
                return;
            }

            HandleLastRow(vertex, column);
        }

        private void HandleFirstRow(Vector3 vertex, int column) =>
            HandleRow(vertex, 0, column, HandleFirstVertexInFirstRow, HandleMiddleVertexInFirstRow, HandleLastVertexInFirstRow);

        private void HandleMiddleRow(Vector3 vertex, int row, int column) =>
            HandleRow(vertex, row, column, HandleFirstVertexInMiddleRow, HandleMiddleVertexInMiddleRow, HandleLastVertexInMiddleRow);

        private void HandleLastRow(Vector3 vertex, int column) =>
            HandleRow(vertex, verticesInRow - 1, column, HandleFirstVertexInLastRow, HandleMiddleVertexInLastRow, HandleLastVertexInLastRow);

        private void HandleRow(Vector3 vertex,
                                               int row,
                                               int column,
                                               Action<Vector3, int, int> firstVertexHandler,
                                               Action<Vector3, int, int> middleVertexHandler,
                                               Action<Vector3, int, int> lastVertexHandler)
        {
            if (column == 0)
            {
                firstVertexHandler(vertex, row, column);
                return;
            }
            if (column < verticesInRow - 1)
            {
                middleVertexHandler(vertex, row, column);
                return;
            }

            lastVertexHandler(vertex, row, column);
        }

        private void HandleFirstVertexInFirstRow(Vector3 vertex, int row, int column)
        {
        }

        private void HandleMiddleVertexInFirstRow(Vector3 vertex, int row, int column)
        {
        }

        private void HandleLastVertexInFirstRow(Vector3 vertex, int row, int column)
        {
        }

        private void HandleFirstVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            HandleUpperRight(vertex, row, column);
        }

        private void HandleMiddleVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            HandleUpperLeft(vertex, row, column);
            HandleUpperRight(vertex, row, column);
        }

        private void HandleLastVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            HandleUpperLeft(vertex, row, column);
        }

        private void HandleFirstVertexInLastRow(Vector3 vertex, int row, int column)
        {
            HandleUpperRight(vertex, row, column);
        }

        private void HandleMiddleVertexInLastRow(Vector3 vertex, int row, int column)
        {
            HandleUpperLeft(vertex, row, column);
            HandleUpperRight(vertex, row, column);
        }

        private void HandleLastVertexInLastRow(Vector3 vertex, int row, int column)
        {
            HandleUpperLeft(vertex, row, column);
        }

        private void HandleOriginal(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
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

        private int Original(int row) => row * rowStep;
        private int UpperDiamond(int row) => row * rowStep + upperDiamondShift;
        private int LowerDiamond(int row) => row * rowStep + lowerDiamondShift;
    }
}