using System;
using System.Collections.Generic;
using System.Linq;
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
        private Vector3[] upperDiamonds;
        private Vector3[] lowerDiamonds;
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
                upperDiamonds = new Vector3[verticesInRow - 1];
                lowerDiamonds = new Vector3[verticesInRow - 1];

                output = inputVertices.SelectMany((vertex, index) => GenerateVerticesFor(vertex, index)).ToArray();
                return output;
            }
        }

        private IEnumerable<Vector3> GenerateVerticesFor(Vector3 vertex, int index)
        {
            var row = index / verticesInRow;
            var column = index % verticesInRow;

            if (row == 0)
            {
                return HandleFirstRow(vertex, column);
            }

            return HandleNonFirstRow(vertex, row, column);
        }

        private IEnumerable<Vector3> HandleNonFirstRow(Vector3 vertex, int row, int column)
        {
            TryCalculateShift(vertex, row, column);
            if (row < verticesInRow - 1) return HandleMiddleRow(vertex, row, column);

            return HandleLastRow(vertex, column);
        }

        private IEnumerable<Vector3> HandleFirstRow(Vector3 vertex, int column) =>
            HandleRow(vertex, 0, column, HandleFirstVertexInFirstRow, HandleMiddleVertexInFirstRow, HandleLastVertexInFirstRow);

        private IEnumerable<Vector3> HandleMiddleRow(Vector3 vertex, int row, int column) =>
            HandleRow(vertex, row, column, HandleFirstVertexInMiddleRow, HandleMiddleVertexInMiddleRow, HandleLastVertexInMiddleRow);

        private IEnumerable<Vector3> HandleLastRow(Vector3 vertex, int column) =>
            HandleRow(vertex, verticesInRow - 1, column, HandleFirstVertexInLastRow, HandleMiddleVertexInLastRow, HandleLastVertexInLastRow);

        private IEnumerable<Vector3> HandleRow(Vector3 vertex,
                                               int row,
                                               int column,
                                               Func<Vector3, int, IEnumerable<Vector3>> firstVertexHandler,
                                               Func<Vector3, int, int, IEnumerable<Vector3>> middleVertexHandler,
                                               Func<Vector3, int, IEnumerable<Vector3>> lastVertexHandler)
        {
            if (column == 0)                return firstVertexHandler(vertex, row);
            if (column < verticesInRow - 1) return middleVertexHandler(vertex, row, column);

            return lastVertexHandler(vertex, row);
        }

        private void TryCalculateShift(Vector3 vertex, int row, int column)
        {
            if ((row == 1) && (column == 1))
            {
                xzShift = (vertex - output[0]) / 2f;
                xzShift.y = 0;
            }
        }

        private IEnumerable<Vector3> HandleFirstVertexInFirstRow(Vector3 vertex, int row)
        {
            yield return vertex;

            output[0] = vertex;
            output[verticesInRow] = vertex;
        }

        private IEnumerable<Vector3> HandleMiddleVertexInFirstRow(Vector3 vertex, int row, int column)
        {
            yield return vertex;

            output[column] = vertex;
            output[verticesInRow + column - 1].y += vertex.y;
            output[verticesInRow + column] = vertex;
        }

        private IEnumerable<Vector3> HandleLastVertexInFirstRow(Vector3 vertex, int row)
        {
            yield return vertex;

            output[verticesInRow - 1] = vertex;
            output[2 * (verticesInRow - 1)].y += vertex.y;
        }

        private IEnumerable<Vector3> HandleFirstVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            if (row == 1)
            {
                output[verticesInRow].y += vertex.y;
            }
            else
            {
                upperDiamonds[0].y += vertex.y;
            }
            lowerDiamonds[0] = vertex;
            yield break;
        }

        private IEnumerable<Vector3> HandleMiddleVertexInMiddleRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;

            yield return CalculateUpperLeftDiamond(vertex, row, column);

            if (row == 1)
            {
                output[verticesInRow + column].y += vertex.y;
            }
            else
            {
                upperDiamonds[column].y += vertex.y;
            }
            lowerDiamonds[column - 1].y += vertex.y;
            lowerDiamonds[column] = vertex;
        }

        private IEnumerable<Vector3> HandleLastVertexInMiddleRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;

            yield return CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);

            lowerDiamonds[verticesInRow - 2].y += vertex.y;

            foreach (var original in GetOriginals(row))
            {
                yield return original;
            }
            upperDiamonds = lowerDiamonds;
            lowerDiamonds = new Vector3[verticesInRow - 1];
        }

        private IEnumerable<Vector3> HandleFirstVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep] = vertex;
            if (row == 1)
            {
                output[verticesInRow].y += vertex.y;
            }
            else
            {
                upperDiamonds[0].y += vertex.y;
            }
            yield break;
        }

        private IEnumerable<Vector3> HandleMiddleVertexInLastRow(Vector3 vertex, int row, int column)
        {
            output[row * rowStep + column] = vertex;
            yield return CalculateUpperLeftDiamond(vertex, row, column);
            if (row == 1)
            {
                output[verticesInRow + column].y += vertex.y;
            }
            else
            {
                upperDiamonds[column].y += vertex.y;
            }
        }

        private IEnumerable<Vector3> HandleLastVertexInLastRow(Vector3 vertex, int row)
        {
            output[row * rowStep + verticesInRow - 1] = vertex;

            yield return CalculateUpperLeftDiamond(vertex, row, verticesInRow - 1);

            foreach (var original in GetOriginals(row))
            {
                yield return original;
            }
        }

        private IEnumerable<Vector3> GetOriginals(int row)
        {

            for (int i = 0; i < verticesInRow; i++)
            {
                yield return output[row * rowStep + i];
            }
        }

        private Vector3 CalculateUpperLeftDiamond(Vector3 vertex, int row, int column)
        {
            if (row == 1)
            {
                output[verticesInRow + column - 1].y += vertex.y;
                output[verticesInRow + column - 1].y /= 4;
                output[verticesInRow + column - 1] += xzShift;
                return output[verticesInRow + column - 1];
            }

            upperDiamonds[column - 1].y += vertex.y;
            upperDiamonds[column - 1].y /= 4;
            upperDiamonds[column - 1] += xzShift;
            return upperDiamonds[column - 1];
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