using ProceduralToolkit.Models.FSMContexts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class RowDuplicator : BaseDiamondGenerator
    {
        public RowDuplicator(Func<IEnumerable<Vector3>, int, FSMContext> contextProvider) : base(contextProvider) { }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var context = ContextProvider?.Invoke(InputVertices, ColumnsInRow).RowDuplicatorContext;
                while (context.State.MoveNext())
                {
                    yield return context.Current;
                }
            }
        }
    }
}
