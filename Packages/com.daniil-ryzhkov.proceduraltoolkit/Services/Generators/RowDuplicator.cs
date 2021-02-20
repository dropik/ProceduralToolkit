using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class RowDuplicator
    {
        public IEnumerable<Vector3> InputVertices
        {
            get => inputVertices ?? (new Vector3[0]);
            set => inputVertices = value;
        }
        private IEnumerable<Vector3> inputVertices;

        public int ColumnsInRow
        {
            get => columnsInRow;
            set => columnsInRow = value < 0 ? 0 : value;
        }
        private int columnsInRow;

        public IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var rowCounter = 0;
                var columnCounter = 0;
                var vertexCopies = new Vector3[ColumnsInRow];
                foreach (var vertex in InputVertices)
                {
                    if ((rowCounter > 0) && (ColumnsInRow > 0))
                    {
                        vertexCopies[columnCounter] = vertex;
                    }

                    yield return vertex;
                    columnCounter++;

                    if (columnCounter >= ColumnsInRow)
                    {
                        columnCounter = 0;
                        if (rowCounter > 0)
                        {
                            foreach (var copy in vertexCopies)
                            {
                                yield return copy;
                            }
                        }
                        rowCounter++;
                    }
                }
            }
        }
    }
}
