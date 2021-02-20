using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public partial class RowDuplicator
    {
        public static RowDuplicator Create()
        {
            return new RowDuplicator((vertices, columns) =>
            {
                var context = new RowDuplicatorContext(columns);

                var enumerator = vertices.GetEnumerator();
                var copiesEnumerator = ((IEnumerable<Vector3>)context.VerticesCopies).GetEnumerator();

                var returnNext = new ReturnNext(enumerator, context);
                var storeCopies = new StoreCopy(enumerator, context);
                var resetAndReturnNext = new ResetAndReturnNext(copiesEnumerator, context);

                returnNext.NextState = storeCopies;
                storeCopies.NextState = resetAndReturnNext;
                resetAndReturnNext.NextState = storeCopies;

                context.State = returnNext;
                return context;
            });
        }
    }
}
