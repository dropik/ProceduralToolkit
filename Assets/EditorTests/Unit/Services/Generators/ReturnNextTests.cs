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
        protected RowDuplicatorContext Context { get; private set; }
        protected Mock<IEnumerator<Vector3>> MockEnumerator { get; private set; }
        private Mock<IRowDuplicatorState> mockState;
        protected ReturnNext ReturnNext { get; private set; }

        [SetUp]
        public void Setup()
        {
            Context = new RowDuplicatorContext(2);
            MockEnumerator = new Mock<IEnumerator<Vector3>>();
            mockState = new Mock<IRowDuplicatorState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            ReturnNext = CreateReturnNext(MockEnumerator.Object, Context, mockState.Object);
        }

        protected virtual ReturnNext CreateReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            RowDuplicatorContext context,
            IRowDuplicatorState nextState)
        {
            return new ReturnNext(inputVerticesEnumerator, context)
            {
                NextState = nextState
            };
        }

        [Test]
        public void TestMoveNextReturnsTrueIfInputEnumeratorHasNext()
        {
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            Assert.That(ReturnNext.MoveNext(), Is.True);
        }

        [Test]
        public void TestMoveNextReturnsFalseIfInputEnumeratorDoesNotHaveNext()
        {
            MockEnumerator.Setup(m => m.MoveNext()).Returns(false);
            Assert.That(ReturnNext.MoveNext(), Is.False);
        }

        [Test]
        public void TestMoveNextSetsContextCurrentToInputCurrent()
        {
            var testVertex = new Vector3(1, 2, 3);
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            MockEnumerator.Setup(m => m.Current).Returns(testVertex);

            ReturnNext.MoveNext();

            Assert.That(Context.Current, Is.EqualTo(testVertex));
        }

        [Test]
        public void TestMoveNextIncrementsColumn()
        {
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            ReturnNext.MoveNext();
            Assert.That(Context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextResetsColumnToZeroIfEndedRow()
        {
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            ReturnNext.MoveNext();
            ReturnNext.MoveNext();
            Assert.That(Context.Column, Is.Zero);
        }

        [Test]
        public void TestMoveNextSetsNextStateIfEndedRow()
        {
            MockEnumerator.Setup(m => m.MoveNext()).Returns(true);
            ReturnNext.MoveNext();
            ReturnNext.MoveNext();
            Assert.That(Context.State.Equals("mock"));
        }
    }
}
