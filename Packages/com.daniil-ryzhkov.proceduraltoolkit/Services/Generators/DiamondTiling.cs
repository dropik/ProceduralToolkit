using ProceduralToolkit.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class DiamondTiling : BaseDiamondGenerator<DiamondTilingContext>
    {
        public DiamondTiling(Func<IEnumerable<Vector3>, int, DiamondTilingContext> contextProvider) : base(contextProvider) { }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var context = ContextProvider.Invoke(InputVertices, ColumnsInRow);
                foreach (var vertex in InputVertices)
                {
                    var next = context.State?.MoveNext(vertex);
                    if (next != null)
                    {
                        yield return (Vector3)next;
                    }
                }
            }
        }
    }
}