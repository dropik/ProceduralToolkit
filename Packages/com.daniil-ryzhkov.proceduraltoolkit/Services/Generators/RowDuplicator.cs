using ProceduralToolkit.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class RowDuplicator
    {
        private readonly Func<IEnumerable<Vector3>, int, RowDuplicatorContext> contextProvider;

        public RowDuplicator(Func<IEnumerable<Vector3>, int, RowDuplicatorContext> contextProvider)
        {
            this.contextProvider = contextProvider;
        }

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
                var context = contextProvider?.Invoke(InputVertices, ColumnsInRow);
                while (context.State.MoveNext())
                {
                    yield return context.Current;
                }
            }
        }
    }
}
