using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreCopyTests : ReturnNextTests
    {
        protected override ReturnNext CreateReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            FSMContext context,
            IRowDuplicatorState nextState)
        {
            return new StoreCopy(inputVerticesEnumerator, context)
            {
                NextState = nextState
            };
        }

        [Test]
        public void TestVertexCopyStored()
        {
            var testVertex = new Vector3(1, 2, 3);
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            MockEnumerator.Setup(m => m.Current).Returns(testVertex);

            ReturnNext.MoveNext();

            Assert.That(Context.RowDuplicatorContext.VerticesCopies[Context.Column - 1], Is.EqualTo(testVertex));
        }

        [Test]
        public void TestVertexStoredIfColumnsInRowIsGraterThanZero()
        {
            var nextState = ReturnNext.NextState;
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            var newContext = new FSMContext(0)
            {
                RowDuplicatorContext = new RowDuplicatorContext(0)
            };
            var storeCopy = CreateReturnNext(MockEnumerator.Object, newContext, nextState);

            try
            {
                storeCopy.MoveNext();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
