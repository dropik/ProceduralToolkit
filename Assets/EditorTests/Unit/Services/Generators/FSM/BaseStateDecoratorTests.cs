using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;
using System.Linq;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    public abstract class BaseStateDecoratorTests
    {
        private Mock<IStateBehaviour> mockState;
        protected FSMSettings Settings { get; private set; }
        protected BaseStateDecorator StateDecorator { get; private set; }

        [SetUp]
        public void Setup()
        {
            mockState = new Mock<IStateBehaviour>();
            Settings = new FSMSettings()
            {
                FSMContext = CreateContext(2)
            };
            StateDecorator = CreateDecorator(mockState.Object, Settings);
        }

        protected virtual FSMContext CreateContext(int columns) => new FSMContext(columns);
        protected abstract BaseStateDecorator CreateDecorator(IStateBehaviour state, FSMSettings settings);

        [Test]
        public void TestWrappeeMoveNextCalled()
        {
            var testVertex = new Vector3(1, 2, 3);
            StateDecorator.MoveNext(testVertex).Count();
            mockState.Verify(m => m.MoveNext(It.Is<Vector3>(v => v == testVertex)), Times.Once);
        }
    }
}
