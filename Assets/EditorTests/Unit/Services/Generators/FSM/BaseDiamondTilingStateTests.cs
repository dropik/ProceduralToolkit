using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    public abstract class BaseDiamondTilingStateTests
    {
        protected Vector3[] InputVertices => new Vector3[]
        {
            new Vector3(1, 2, 0),
            new Vector3(2, 0, 0)
        };
        protected FSMContext Context { get; private set; }
        private Mock<IDiamondTilingState> mockNextStateWhenRowEnded;
        private Mock<IDiamondTilingState> mockNextStateWhenRowContinues;
        protected BaseDiamondTilingState ReturnVertex { get; private set; }

        [SetUp]
        public void Setup()
        {
            Context = CreateContext(columns: 2);

            mockNextStateWhenRowEnded = new Mock<IDiamondTilingState>();
            mockNextStateWhenRowContinues = new Mock<IDiamondTilingState>();
            mockNextStateWhenRowEnded.Setup(m => m.Equals(It.Is<string>(s => s == "ended"))).Returns(true);
            mockNextStateWhenRowContinues.Setup(m => m.Equals(It.Is<string>(s => s == "continue"))).Returns(true);

            ReturnVertex = GetReturnVertex(Context);
            ReturnVertex.StateWhenEndedRow = mockNextStateWhenRowEnded.Object;
            ReturnVertex.StateWhenRowContinues = mockNextStateWhenRowContinues.Object;
        }

        protected abstract FSMContext CreateContext(int columns);
        protected abstract BaseDiamondTilingState GetReturnVertex(FSMContext context);

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
