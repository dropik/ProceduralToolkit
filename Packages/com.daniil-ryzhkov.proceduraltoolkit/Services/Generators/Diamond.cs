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

        private Vector3[] originals;
        private Vector3[] upperDiamonds;
        private Vector3[] lowerDiamonds;
        private Vector3 first = Vector3.zero;
        private Vector3 xzShift = Vector3.zero;

        public Diamond(IEnumerable<Vector3> inputVertices, int iteration)
        {
            this.inputVertices = inputVertices;
            this.iteration = iteration;
            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                originals = new Vector3[verticesInRow];
                upperDiamonds = new Vector3[verticesInRow - 1];
                lowerDiamonds = new Vector3[verticesInRow - 1];

                return inputVertices.SelectMany((vertex, index) => GenerateVerticesFor(vertex, index));
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
            if (row < verticesInRow - 1) return HandleRow(vertex, column);

            return HandleLastRow(vertex, column);
        }

        private IEnumerable<Vector3> HandleFirstRow(Vector3 vertex, int column)
        {
            if (column == 0)                return HandleFirstVertex(vertex);
            if (column < verticesInRow - 1) return HandleVertexInFirstRow(vertex, column);

            return HandleLastVertexInFirstRow(vertex);
        }

        private IEnumerable<Vector3> HandleRow(Vector3 vertex, int column)
        {
            if (column == 0)                return HandleFirstVertexInRow(vertex);
            if (column < verticesInRow - 1) return HandleVertexInRow(vertex, column);

            return HandleLastVertexInRow(vertex);
        }

        private IEnumerable<Vector3> HandleLastRow(Vector3 vertex, int column)
        {
            if (column == 0)                return HandleFirstVertexInLastRow(vertex);
            if (column < verticesInRow - 1) return HandleVertexInLastRow(vertex, column);

            return HandleLastVertexInLastRow(vertex);
        }

        private void TryCalculateShift(Vector3 vertex, int row, int column)
        {
            if ((row == 1) && (column == 1))
            {
                xzShift = (vertex - first) / 2f;
                xzShift.y = 0;
            }
        }

        private IEnumerable<Vector3> HandleFirstVertex(Vector3 vertex)
        {
            yield return vertex;
            first = vertex;
            lowerDiamonds[0] = vertex;
        }

        private IEnumerable<Vector3> HandleVertexInFirstRow(Vector3 vertex, int index)
        {
            yield return vertex;
            lowerDiamonds[index - 1].y += vertex.y;
            lowerDiamonds[index] = vertex;
        }

        private IEnumerable<Vector3> HandleLastVertexInFirstRow(Vector3 vertex)
        {
            yield return vertex;
            lowerDiamonds[verticesInRow - 2].y += vertex.y;
            upperDiamonds = lowerDiamonds;
            lowerDiamonds = new Vector3[verticesInRow - 1];
        }

        private IEnumerable<Vector3> HandleFirstVertexInRow(Vector3 vertex)
        {
            originals[0] = vertex;
            upperDiamonds[0].y += vertex.y;
            lowerDiamonds[0] = vertex;
            yield break;
        }

        private IEnumerable<Vector3> HandleVertexInRow(Vector3 vertex, int column)
        {
            originals[column] = vertex;

            upperDiamonds[column - 1].y += vertex.y;
            upperDiamonds[column - 1].y /= 4;
            upperDiamonds[column - 1] += xzShift;
            yield return upperDiamonds[column - 1];

            upperDiamonds[column].y += vertex.y;
            lowerDiamonds[column - 1].y += vertex.y;
            lowerDiamonds[column] = vertex;
        }

        private IEnumerable<Vector3> HandleLastVertexInRow(Vector3 vertex)
        {
            originals[verticesInRow - 1] = vertex;

            upperDiamonds[verticesInRow - 2].y += vertex.y;
            upperDiamonds[verticesInRow - 2].y /= 4;
            upperDiamonds[verticesInRow - 2] += xzShift;
            yield return upperDiamonds[verticesInRow - 2];

            lowerDiamonds[verticesInRow - 2].y += vertex.y;

            foreach (var original in originals)
            {
                yield return original;
            }
            upperDiamonds = lowerDiamonds;
            lowerDiamonds = new Vector3[verticesInRow - 1];
        }

        private IEnumerable<Vector3> HandleFirstVertexInLastRow(Vector3 vertex)
        {
            originals[0] = vertex;
            upperDiamonds[0].y += vertex.y;
            yield break;
        }

        private IEnumerable<Vector3> HandleVertexInLastRow(Vector3 vertex, int column)
        {
            originals[column] = vertex;

            upperDiamonds[column - 1].y += vertex.y;
            upperDiamonds[column - 1].y /= 4;
            upperDiamonds[column - 1] += xzShift;
            yield return upperDiamonds[column - 1];

            upperDiamonds[column].y += vertex.y;
        }

        private IEnumerable<Vector3> HandleLastVertexInLastRow(Vector3 vertex)
        {
            originals[verticesInRow - 1] = vertex;

            upperDiamonds[verticesInRow - 2].y += vertex.y;
            upperDiamonds[verticesInRow - 2].y /= 4;
            upperDiamonds[verticesInRow - 2] += xzShift;
            yield return upperDiamonds[verticesInRow - 2];

            foreach (var original in originals)
            {
                yield return original;
            }
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