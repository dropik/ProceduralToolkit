using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ResetAndReturnNextTests : ReturnNextTests
    {
        protected override ReturnNext CreateReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            FSMContext context,
            IRowDuplicatorState nextState)
        {
            return new ResetAndReturnNext(inputVerticesEnumerator, context)
            {
                NextState = nextState
            };
        }

        [Test]
        public void TestEnumeratorResetIfColumnIsZero()
        {
            ReturnNext.MoveNext();
            MockEnumerator.Verify(m => m.Reset(), Times.Once);
        }

        [Test]
        public void TestEnumeratorResetNotCalledIfColumnIsNotZero()
        {
            Context.Column = 1;
            ReturnNext.MoveNext();
            MockEnumerator.Verify(m => m.Reset(), Times.Never);
        }
    }
}
