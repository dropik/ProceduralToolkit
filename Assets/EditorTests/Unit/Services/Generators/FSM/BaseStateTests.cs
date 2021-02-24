using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    public abstract class BaseStateTests
    {
        protected Vector3[] InputVertices => new Vector3[]
        {
            new Vector3(1, 2, 0),
            new Vector3(2, 0, 0)
        };
        protected FSMSettings Settings { get; private set; }
        private Mock<IStateBehaviour> mockStateWhenLimitReached;
        private Mock<IStateBehaviour> mockNextState;
        protected BaseState ReturnVertex { get; private set; }

        [SetUp]
        public void Setup()
        {
            Settings = new FSMSettings()
            {
                FSMContext = CreateContext(columns: 3),
                ColumnsLimit = 2
            };

            mockNextState = new Mock<IStateBehaviour>();
            mockStateWhenLimitReached = new Mock<IStateBehaviour>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "continue"))).Returns(true);
            mockStateWhenLimitReached.Setup(m => m.Equals(It.Is<string>(s => s == "ended"))).Returns(true);

            ReturnVertex = GetReturnVertex(Settings);
            ReturnVertex.NextState = mockNextState.Object;
            ReturnVertex.StateWhenLimitReached = mockStateWhenLimitReached.Object;
        }

        protected abstract FSMContext CreateContext(int columns);
        protected abstract BaseState GetReturnVertex(FSMSettings settings);

        [Test]
        public void TestMoveNextIncrementsColumn()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Settings.FSMContext.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestMoveNextSetsAppropriateNextState()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Settings.FSMContext.State.Equals("continue"));
        }

        [Test]
        public void TestMoveNextZeroesColumnIfLimitReachedAndZeroFlagIsSet()
        {
            var context = Settings.FSMContext;
            context.Column = 1;

            ReturnVertex.MoveNext(InputVertices[1]);

            Assert.That(context.Column, Is.Zero);
        }

        [Test]
        public void TestMoveNextDoesNotZeroColumnIfLimitReachedButZeroFlagIsNotSet()
        {
            var context = Settings.FSMContext;
            context.Column = 1;
            Settings.ZeroColumnOnLimitReached = false;

            ReturnVertex.MoveNext(InputVertices[1]);

            Assert.That(context.Column, Is.Not.Zero);
        }

        [Test]
        public void TestMoveNextChangesStateIfLimitReached()
        {
            Settings.FSMContext.Column = 1;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Settings.FSMContext.State.Equals("ended"));
        }
    }
}
