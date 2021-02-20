using ProceduralToolkit.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class RowDuplicator : BaseDiamondGenerator<RowDuplicatorContext>
    {
        public RowDuplicator(Func<IEnumerable<Vector3>, int, RowDuplicatorContext> contextProvider) : base(contextProvider) { }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var context = ContextProvider?.Invoke(InputVertices, ColumnsInRow);
                while (context.State.MoveNext())
                {
                    yield return context.Current;
                }
            }
        }
    }
}
