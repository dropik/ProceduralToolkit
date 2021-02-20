using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class RowDuplicatorTests
    {
        private RowDuplicator duplicator;
        private Mock<IRowDuplicatorState> mockState;
        private RowDuplicatorContext context;

        [SetUp]
        public void Setup()
        {
            mockState = new Mock<IRowDuplicatorState>();
            context = new RowDuplicatorContext(2)
            {
                State = mockState.Object
            };
            duplicator = new RowDuplicator((vertices, columns) => context);
        }

        [Test]
        public void TestOnInputVerticesNotSet()
        {
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, duplicator.InputVertices);
        }

        [Test]
        public void TestOnNegativeColumnsInRow()
        {
            duplicator.ColumnsInRow = -2;
            Assert.That(duplicator.ColumnsInRow, Is.Zero);
        }

        [Test]
        public void TestStateMoveNextUsed()
        {
            var enumerator = duplicator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            mockState.Verify(m => m.MoveNext(), Times.Once);
        }

        [Test]
        public void TestEnumerableFinishesWhenStateMoveNextReturnsFalse()
        {
            mockState.Setup(m => m.MoveNext()).Returns(false);
            var enumerator = duplicator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
        }

        [Test]
        public void TestEnumerableReturnsContextCurrent()
        {
            mockState.Setup(m => m.MoveNext()).Returns(true);
            var testVertex = new Vector3(1, 2, 3);
            context.Current = testVertex;
            var enumerator = duplicator.OutputVertices.GetEnumerator();

            enumerator.MoveNext();

            Assert.That(enumerator.Current, Is.EqualTo(testVertex));
        }
    }
}
