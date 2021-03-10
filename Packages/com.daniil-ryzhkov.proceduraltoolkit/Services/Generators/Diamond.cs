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
            output[Original(row) + column] = vertex;
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleMiddleVertexInFirstRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            output[LowerDiamond(row) + column - 1].y += vertex.y;
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleLastVertexInFirstRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            output[LowerDiamond(row) + column - 1].y += vertex.y;
        }

        private void HandleFirstVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            output[UpperDiamond(row) + column].y += vertex.y;
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleMiddleVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;

            CalculateUpperLeftDiamond(vertex, row, column);

            output[UpperDiamond(row) + column].y += vertex.y;
            output[LowerDiamond(row) + column - 1].y += vertex.y;
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleLastVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            CalculateUpperLeftDiamond(vertex, row, column);
            output[LowerDiamond(row) + column - 1].y += vertex.y;
        }

        private void HandleFirstVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            output[UpperDiamond(row) + column].y += vertex.y;
        }

        private void HandleMiddleVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            CalculateUpperLeftDiamond(vertex, row, column);
            output[UpperDiamond(row) + column].y += vertex.y;
        }

        private void HandleLastVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
            CalculateUpperLeftDiamond(vertex, row, column);
        }

        private void CalculateUpperLeftDiamond(Vector3 vertex, int row, int column)
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