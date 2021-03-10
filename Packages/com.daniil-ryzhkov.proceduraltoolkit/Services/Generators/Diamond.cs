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
                                               Action<Vector3, int> firstVertexHandler,
                                               Action<Vector3, int, int> middleVertexHandler,
                                               Action<Vector3, int> lastVertexHandler)
        {
            if (column == 0)
            {
                firstVertexHandler(vertex, row);
                return;
            }
            if (column < verticesInRow - 1)
            {
                middleVertexHandler(vertex, row, column);
                return;
            }

            lastVertexHandler(vertex, row);
        }

        private void HandleFirstVertexInFirstRow(Vector3 vertex, int row)
        {
            output[0] = vertex;
            output[lowerDiamondShift] = vertex;
        }

        private void HandleMiddleVertexInFirstRow(Vector3 vertex, int row, int column)
        {
            output[column] = vertex;
            output[lowerDiamondShift + column - 1].y += vertex.y;
            output[lowerDiamondShift + column] = vertex;
        }

        private void HandleLastVertexInFirstRow(Vector3 vertex, int row)
        {
            output[verticesInRow - 1] = vertex;
            output[lowerDiamondShift + verticesInRow - 2].y += vertex.y;
        }

        private void HandleFirstVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            output[row * rowStep + upperDiamondShift].y += vertex.y;
            output[row * rowStep + lowerDiamondShift] = vertex;
        }

        private void HandleMiddleVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;

            CalculateUpperLeftDiamond(vertex, row, column);

            output[row * rowStep + upperDiamondShift + column].y += vertex.y;
            output[row * rowStep + lowerDiamondShift + column - 1].y += vertex.y;
            output[row * rowStep + lowerDiamondShift + column] = vertex;
        }

        private void HandleLastVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;
            CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);
            output[row * rowStep + lowerDiamondShift + verticesInRow - 2].y += vertex.y;
        }

        private void HandleFirstVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            output[row * rowStep + upperDiamondShift].y += vertex.y;
        }

        private void HandleMiddleVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;
            CalculateUpperLeftDiamond(vertex, row, column);
            output[row * rowStep + upperDiamondShift + column].y += vertex.y;
        }

        private void HandleLastVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;
            CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);
        }

        private void CalculateUpperLeftDiamond(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + upperDiamondShift + column - 1].y += vertex.y;
            output[row * rowStep + upperDiamondShift + column - 1].y /= 4;
            output[row * rowStep + upperDiamondShift + column - 1] += xzShift;
        }
    }
}