using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondTiling
    {
        private readonly IEnumerable<Vector3> inputVertices;
        private readonly int length;

        public DiamondTiling(IEnumerable<Vector3> inputVertices, int length)
        {
            this.inputVertices = inputVertices;
            this.length = length;
        }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var rowCounter = 0;
                var columnCounter = 0;
                var originalVertices = new Vector3[length];
                var first = Vector3.zero;
                var xzShift = Vector3.zero;

                foreach (var vertex in inputVertices)
                {
                    if (rowCounter == 0)
                    {
                        yield return vertex;

                        if (columnCounter == 0)
                        {
                            first = vertex;
                        }
                    }
                    else
                    {
                        originalVertices[columnCounter] = vertex;

                        if ((rowCounter == 1) && (columnCounter == 1))
                        {
                            xzShift = (first - vertex) / 2;
                            xzShift.y = 0;
                        }

                        if (columnCounter > 0)
                        {
                            var newVertex = new Vector3(vertex.x, 0, vertex.z);
                            newVertex += xzShift;
                            yield return newVertex;
                        }
                    }

                    columnCounter++;
                    if (columnCounter >= length)
                    {
                        columnCounter = 0;
                        
                        if (rowCounter > 0)
                        {
                            foreach (var original in originalVertices)
                            {
                                yield return original;
                            }
                        }

                        rowCounter++;
                    }
                }
            }
        }
    }
}