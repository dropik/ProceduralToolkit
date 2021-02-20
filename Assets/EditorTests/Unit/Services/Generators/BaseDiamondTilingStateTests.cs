using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class BaseDiamondTilingStateTests
    {
        protected Vector3[] InputVertices => new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        protected DiamondTilingContext Context { get; private set; }
        private Mock<IDiamondTilingState> mockNextStateWhenRowEnded;
        private Mock<IDiamondTilingState> mockNextStateWhenRowContinues;
        protected BaseDiamondTilingState ReturnVertex { get; private set; }

        [SetUp]
        public void Setup()
        {
            Context = new DiamondTilingContext(2);

            mockNextStateWhenRowEnded = new Mock<IDiamondTilingState>();
            mockNextStateWhenRowContinues = new Mock<IDiamondTilingState>();
            mockNextStateWhenRowEnded.Setup(m => m.Equals(It.Is<string>(s => s == "ended"))).Returns(true);
            mockNextStateWhenRowContinues.Setup(m => m.Equals(It.Is<string>(s => s == "continue"))).Returns(true);

            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
        }

        protected abstract BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context);

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
