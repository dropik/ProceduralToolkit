using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly IEnumerable<Vector3> inputVertices;
        private readonly int iteration;
        private readonly int verticesInRow;
        private readonly int rowStep;

        private Vector3[] output;
        private Vector3 xzShift = Vector3.zero;

        public Diamond(IEnumerable<Vector3> inputVertices, int iteration)
        {
            this.inputVertices = inputVertices;
            this.iteration = iteration;
            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;
            rowStep = 2 * verticesInRow - 1;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                output = new Vector3[2 * verticesInRow * (verticesInRow - 1) + 1];
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
            TryCalculateShift(vertex, row, column);
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

        private void TryCalculateShift(Vector3 vertex, int row, int column)
        {
            if ((row == 1) && (column == 1))
            {
                xzShift = (vertex - output[0]) / 2f;
                xzShift.y = 0;
            }
        }

        private void HandleFirstVertexInFirstRow(Vector3 vertex, int row)
        {
            output[0] = vertex;
            output[verticesInRow] = vertex;
        }

        private void HandleMiddleVertexInFirstRow(Vector3 vertex, int row, int column)
        {
            output[column] = vertex;
            output[verticesInRow + column - 1].y += vertex.y;
            output[verticesInRow + column] = vertex;
        }

        private void HandleLastVertexInFirstRow(Vector3 vertex, int row)
        {
            output[verticesInRow - 1] = vertex;
            output[2 * (verticesInRow - 1)].y += vertex.y;
        }

        private void HandleFirstVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            output[(row - 1) * rowStep + verticesInRow].y += vertex.y;
            output[row * rowStep + verticesInRow] = vertex;
        }

        private void HandleMiddleVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;

            CalculateUpperLeftDiamond(vertex, row, column);

            output[(row - 1) * rowStep + verticesInRow + column].y += vertex.y;
            output[row * rowStep + verticesInRow + column - 1].y += vertex.y;
            output[row * rowStep + verticesInRow + column] = vertex;
        }

        private void HandleLastVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;
            CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);
            output[row * rowStep + 2 * (verticesInRow - 1)].y += vertex.y;
        }

        private void HandleFirstVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            output[(row - 1) * rowStep + verticesInRow].y += vertex.y;
        }

        private void HandleMiddleVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;
            CalculateUpperLeftDiamond(vertex, row, column);
            output[(row - 1) * rowStep + verticesInRow + column].y += vertex.y;
        }

        private void HandleLastVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;
            CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);
        }

        private void CalculateUpperLeftDiamond(Vector3 vertex, int row, int column)
        {
            output[(row - 1) * rowStep + verticesInRow + column - 1].y += vertex.y;
            output[(row - 1) * rowStep + verticesInRow + column - 1].y /= 4;
            output[(row - 1) * rowStep + verticesInRow + column - 1] += xzShift;
        }

        public IEnumerable<bool> NewVertices
        {
            get
            {
                var rowCounter = 0;
                var columnCounter = 0;
                var oldVerticesInRow = Mathf.Pow(2, iteration) + 1;
                var newVerticesInRow = oldVerticesInRow - 1;
                var totalVerticesInRow = oldVerticesInRow + newVerticesInRow;

                while (rowCounter < oldVerticesInRow)
                {
                    if (columnCounter < oldVerticesInRow)
                    {
                        yield return false;
                    }
                    else if (rowCounter < oldVerticesInRow - 1)
                    {
                        yield return true;
                    }

                    columnCounter++;
                    if (columnCounter >= totalVerticesInRow)
                    {
                        columnCounter = 0;
                        rowCounter++;
                    }
                }
            }
        }
    }
}