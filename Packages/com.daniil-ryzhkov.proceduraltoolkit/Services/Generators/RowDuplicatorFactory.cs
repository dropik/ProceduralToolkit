using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
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
                var context = new FSMContext(columns)
                {
                    RowDuplicatorContext = new RowDuplicatorContext(columns)
                };

                var enumerator = vertices.GetEnumerator();
                var copiesEnumerator = ((IEnumerable<Vector3>)context.RowDuplicatorContext.VerticesCopies).GetEnumerator();

                var returnNext = new ReturnNext(enumerator, context);
                var storeCopies = new StoreCopy(enumerator, context);
                var resetAndReturnNext = new ResetAndReturnNext(copiesEnumerator, context);

                returnNext.NextState = storeCopies;
                storeCopies.NextState = resetAndReturnNext;
                resetAndReturnNext.NextState = storeCopies;

                context.RowDuplicatorContext.State = returnNext;
                return context;
            });
        }
    }
}
