using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class BaseReturnVertexTests
    {
        protected Vector3[] InputVertices => new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        protected DiamondContext Context { get; private set; }
        private Mock<IState> mockNextStateWhenRowEnded;
        private Mock<IState> mockNextStateWhenRowContinues;
        protected BaseReturnVertex ReturnVertex { get; private set; }

        [SetUp]
        public void Setup()
        {
            Context = new DiamondContext(2)
            {
                XZShift = new Vector3(0.5f, 0, 0.5f)
            };
            mockNextStateWhenRowEnded = new Mock<IState>();
            mockNextStateWhenRowContinues = new Mock<IState>();
            mockNextStateWhenRowEnded.Setup(m => m.Equals(It.Is<string>(s => s == "ended"))).Returns(true);
            mockNextStateWhenRowContinues.Setup(m => m.Equals(It.Is<string>(s => s == "continue"))).Returns(true);
            var enumerator = ((IEnumerable<Vector3>)InputVertices).GetEnumerator();
            ReturnVertex = GetReturnVertex(enumerator, Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
        }

        protected abstract BaseReturnVertex GetReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context);
        protected abstract BaseReturnVertex GetReturnVertex(DiamondContext context);

        [Test]
        public void TestNextStateDidNotChangeIfNotEndedColumn()
        {
            ReturnVertex.MoveNext();
            Assert.That(Context.State, Is.Null);
        }

        [Test]
        public void TestNextStateSetIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.State.Equals("ended"));
        }

        [Test]
        public void TestColumnZeroedIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.Column, Is.Zero);
        }

        [Test]
        public void TestRowIncrementedIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.Row, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextIncrementsColumn()
        {
            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextSetsAppropriateStateWhenColumnContinues()
        {
            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.State.Equals("continue"));
        }

        [Test]
        public void TestMoveNextZeroesColumnIfEndedRow()
        {
            Context.Column = 1;
            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Context.Column, Is.Zero);
        }

        [Test]
        public void TestMoveNextChangesStateIfEndedRow()
        {
            Context.Column = 1;
            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Context.State.Equals("ended"));
        }
    }
}
