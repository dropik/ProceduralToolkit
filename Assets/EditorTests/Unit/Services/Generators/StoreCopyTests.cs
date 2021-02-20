using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class StoreCopyTests : ReturnNextTests
    {
        protected override ReturnNext CreateReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            RowDuplicatorContext context,
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

            Assert.That(Context.VerticesCopies[Context.Column - 1], Is.EqualTo(testVertex));
        }
    }
}
