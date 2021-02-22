using ProceduralToolkit.Models.FSMContexts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class RowDuplicator : ColumnsBasedGenerator
    {
        private readonly Func<IEnumerable<Vector3>, int, FSMContext> contextProvider;

        public RowDuplicator(Func<IEnumerable<Vector3>, int, FSMContext> contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var context = contextProvider?.Invoke(InputVertices, ColumnsInRow).RowDuplicatorContext;
                while (context.State.MoveNext())
                {
                    yield return context.Current;
                }
            }
        }
    }
}
