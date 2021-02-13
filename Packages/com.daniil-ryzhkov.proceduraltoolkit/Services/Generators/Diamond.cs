using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly IEnumerable<Vector3> inputVertices;
        private readonly int iteration;

        public Diamond(IEnumerable<Vector3> inputVertices, int iteration)
        {
            this.inputVertices = inputVertices;
            this.iteration = iteration;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var rowCounter = 0;
                var columnCounter = 0;
                var verticesInRow = (int)Mathf.Pow(2, iteration) + 1;
                var currentOriginalVertices = new Vector3[verticesInRow];
                var currentDiamondVertices = new Vector3[verticesInRow - 1];
                var nextDiamondVertices = new Vector3[verticesInRow - 1];
                var first = Vector3.zero;
                var x = Vector3.zero;
                var z = Vector3.zero;
                var d = Vector3.zero;

                foreach (var vertex in inputVertices)
                {
                    if (rowCounter == 0)
                    {
                        yield return vertex;

                        if (columnCounter == 0)
                        {
                            first = vertex;
                        }
                        else if (columnCounter == 1)
                        {
                            x = vertex - first;
                        }
                    }

                    if ((rowCounter == 1) && (columnCounter == 0))
                    {
                        z = vertex - first;
                        d = x + z;
                    }

                    if (rowCounter > 0)
                    {
                        currentOriginalVertices[columnCounter] = vertex;

                        if (columnCounter > 0)
                        {
                            currentDiamondVertices[columnCounter - 1].y += vertex.y;
                            currentDiamondVertices[columnCounter - 1].y /= 4;

                            var xz = vertex - d/2;
                            currentDiamondVertices[columnCounter - 1].x = xz.x;
                            currentDiamondVertices[columnCounter - 1].z = xz.z;

                            yield return currentDiamondVertices[columnCounter - 1];
                        }

                        if (columnCounter < verticesInRow - 1)
                        {
                            currentDiamondVertices[columnCounter].y += vertex.y;
                        }
                    }

                    if (rowCounter < verticesInRow - 1)
                    {
                        if (columnCounter > 0)
                        {
                            nextDiamondVertices[columnCounter - 1].y += vertex.y;
                        }

                        if (columnCounter < verticesInRow - 1)
                        {
                            nextDiamondVertices[columnCounter] = new Vector3(0, vertex.y, 0);
                        }
                    }

                    columnCounter++;
                    if (columnCounter >= verticesInRow)
                    {
                        if (rowCounter > 0)
                        {
                            foreach (var original in currentOriginalVertices)
                            {
                                yield return original;
                            }
                        }

                        currentDiamondVertices = nextDiamondVertices;
                        nextDiamondVertices = new Vector3[verticesInRow - 1];

                        columnCounter = 0;
                        rowCounter++;
                    }
                }
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