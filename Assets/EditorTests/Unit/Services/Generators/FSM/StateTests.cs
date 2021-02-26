using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StateTests
    {
        private State state;
        private Mock<IVertexPreprocessor> mockPreprocessor;
        private Mock<IStateOutput> mockOutput;
        private Mock<ITransitionBehaviour> mockTransitionBehaviour;

        [SetUp]
        public void Setup()
        {
            mockPreprocessor = new Mock<IVertexPreprocessor>();
            mockOutput = new Mock<IStateOutput>();
            mockTransitionBehaviour = new Mock<ITransitionBehaviour>();

            state = new State(mockOutput.Object, mockTransitionBehaviour.Object)
            {
                VertexPreprocessor = mockPreprocessor.Object
            };
        }

        [Test]
        public void TestPreprocessorCalled()
        {
            var input = new Vector3(1, 2, 3);
            state.VertexPreprocessor = mockPreprocessor.Object;
            state.MoveNext(input);
            mockPreprocessor.Verify(m => m.Process(It.Is<Vector3>(v => v == input)), Times.Once);
        }

        [Test]
        public void TestStateOutputResultReturned()
        {
            var expectedResult = new Vector3[]
            {
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0),
                new Vector3(3, 0, 0)
            };
            mockOutput.Setup(m => m.GetOutputFor(It.IsAny<Vector3>())).Returns(expectedResult);
            var input = new Vector3(1, 2, 3);

            var result = state.MoveNext(input);

            CollectionAssert.AreEqual(expectedResult, result);
            mockOutput.Verify(m => m.GetOutputFor(It.Is<Vector3>(v => v == input)), Times.Once);
        }

        [Test]
        public void TestOnNullPreprocessor()
        {
            state.VertexPreprocessor = null;
            state.MoveNext(default);
        }

        [Test]
        public void TestTransitionBehaviourExecuted()
        {
            state.MoveNext(default);
            mockTransitionBehaviour.Verify(m => m.Execute(), Times.Once);
        }
    }
}