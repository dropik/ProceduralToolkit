using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
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
            Context = new DiamondContext(2);
            SetupContext(Context);

            mockNextStateWhenRowEnded = new Mock<IState>();
            mockNextStateWhenRowContinues = new Mock<IState>();
            mockNextStateWhenRowEnded.Setup(m => m.Equals(It.Is<string>(s => s == "ended"))).Returns(true);
            mockNextStateWhenRowContinues.Setup(m => m.Equals(It.Is<string>(s => s == "continue"))).Returns(true);

            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
        }

        protected virtual void SetupContext(DiamondContext context) { }

        protected abstract BaseReturnVertex GetReturnVertex(DiamondContext context);

        [Test]
        public void TestMoveNextIncrementsColumn()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextSetsAppropriateStateWhenColumnContinues()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.State.Equals("continue"));
        }

        [Test]
        public void TestMoveNextZeroesColumnIfEndedRow()
        {
            Context.Column = 1;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Context.Column, Is.Zero);
        }

        [Test]
        public void TestMoveNextChangesStateIfEndedRow()
        {
            Context.Column = 1;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Context.State.Equals("ended"));
        }
    }
}
