using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnNextTests
    {
        private RowDuplicatorContext context;
        private Mock<IEnumerator<Vector3>> mockEnumerator;
        private Mock<IRowDuplicatorState> mockState;
        private ReturnNext returnNext;

        [SetUp]
        public void Setup()
        {
            context = new RowDuplicatorContext(2);
            mockEnumerator = new Mock<IEnumerator<Vector3>>();
            mockState = new Mock<IRowDuplicatorState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            returnNext = new ReturnNext(mockEnumerator.Object, context)
            {
                NextState = mockState.Object
            };
        }

        [Test]
        public void TestMoveNextReturnsTrueIfInputEnumeratorHasNext()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            Assert.That(returnNext.MoveNext(), Is.True);
        }

        [Test]
        public void TestMoveNextReturnsFalseIfInputEnumeratorDoesNotHaveNext()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(false);
            Assert.That(returnNext.MoveNext(), Is.False);
        }

        [Test]
        public void TestMoveNextSetsContextCurrentToInputCurrent()
        {
            var testVertex = new Vector3(1, 2, 3);
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            mockEnumerator.Setup(m => m.Current).Returns(testVertex);

            returnNext.MoveNext();

            Assert.That(context.Current, Is.EqualTo(testVertex));
        }

        [Test]
        public void TestMoveNextIncrementsColumn()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            returnNext.MoveNext();
            Assert.That(context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextResetsColumnToZeroIfEndedColumn()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            returnNext.MoveNext();
            returnNext.MoveNext();
            Assert.That(context.Column, Is.Zero);
        }

        [Test]
        public void TestMoveNextIncrementsRowIfEndedColumnd()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            returnNext.MoveNext();
            returnNext.MoveNext();
            Assert.That(context.Row, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextSetsNextStateIfEndedColumn()
        {
            mockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            returnNext.MoveNext();
            returnNext.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }
    }
}
