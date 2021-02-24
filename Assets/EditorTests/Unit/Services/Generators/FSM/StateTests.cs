using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StateTests
    {
        private Mock<IStateBehaviour> mockBehaviour;
        private FSMContext context;
        private Mock<IList<Transition>> mockList;
        private Mock<ITransitionBuilder> mockTransitionBuilder;
        private Mock<Func<bool>> mockCondition;
        private State state;

        [SetUp]
        public void Setup()
        {
            mockBehaviour = new Mock<IStateBehaviour>();
            context = new FSMContext(2);

            mockList = new Mock<IList<Transition>>();
            mockList.Setup(m => m.GetEnumerator()).Returns(((IEnumerable<Transition>)new Transition[0]).GetEnumerator());

            mockTransitionBuilder = new Mock<ITransitionBuilder>();
            mockTransitionBuilder.Setup(m => m.Equals(It.Is<string>(s => s == "builder"))).Returns(true);

            mockCondition = new Mock<Func<bool>>();
            mockCondition.Setup(m => m.Invoke()).Returns(true);

            state = new State(mockBehaviour.Object, context, mockList.Object, transition => mockTransitionBuilder.Object);
        }

        [Test]
        public void TestCorrectTransitionBuilderCreated()
        {
            var result = state.On(null);
            Assert.That(result.Equals("builder"));
        }

        [Test]
        public void TestCorrectTransitionGivenToProvider()
        {
            var mockProvier = new Mock<Func<Transition, ITransitionBuilder>>();
            state = new State(mockBehaviour.Object, context, mockList.Object, mockProvier.Object);

            state.On(mockCondition.Object);

            mockProvier.Verify(m => m.Invoke(It.Is<Transition>(t => t.Condition())), Times.Once);
        }

        [Test]
        public void TestCorrectTransitionAddedToList()
        {
            state.On(mockCondition.Object);
            mockList.Verify(m => m.Add(It.Is<Transition>(t => t.Condition())), Times.Once);
        }

        [Test]
        public void TestBehaviourResultReturned()
        {
            var expectedResult = new Vector3[]
            {
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0),
                new Vector3(3, 0, 0)
            };
            mockBehaviour.Setup(m => m.MoveNext(It.IsAny<Vector3>())).Returns(expectedResult);

            var result = state.MoveNext(Vector3.zero);

            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestCorrectInputVertexPassed()
        {
            var input = new Vector3(1, 2, 3);
            state.MoveNext(input);
            mockBehaviour.Verify(m => m.MoveNext(It.Is<Vector3>(v => v == input)), Times.Once);
        }

        [Test]
        public void TestDefaultStateSetIfNoTransitionsCreated()
        {
            var mockState = new Mock<IState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            state.SetDefaultNext(mockState.Object);

            state.MoveNext(Vector3.zero);

            Assert.That(context.State.Equals("mock"));
        }

        [Test]
        public void TestStateSetFromTransition()
        {
            var mockState = new Mock<IState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);

            var mockEnumerator = new Mock<IEnumerator<Transition>>();
            mockEnumerator.SetupSequence(m => m.MoveNext()).Returns(true).Returns(false);
            mockEnumerator.Setup(m => m.Current).Returns(new Transition()
            {
                Condition = mockCondition.Object,
                NextState = mockState.Object
            });
            mockList.Setup(m => m.GetEnumerator()).Returns(mockEnumerator.Object);

            state.MoveNext(Vector3.zero);

            Assert.That(context.State.Equals("mock"));
        }
    }
}
