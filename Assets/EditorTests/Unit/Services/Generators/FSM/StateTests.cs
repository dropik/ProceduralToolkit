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
        private Mock<IVertexPreprocessor> mockPreprocessor;
        private Mock<IStateOutput> mockOutput;
        private FSMContext context;
        private Mock<IList<Transition>> mockList;
        private Mock<ITransitionBuilder> mockTransitionBuilder;
        private Mock<Func<bool>> mockCondition;
        private Mock<IState> mockState;
        private State state;

        [SetUp]
        public void Setup()
        {
            mockPreprocessor = new Mock<IVertexPreprocessor>();
            mockOutput = new Mock<IStateOutput>();
            context = new FSMContext();

            mockList = new Mock<IList<Transition>>();
            mockList.Setup(m => m.GetEnumerator()).Returns(((IEnumerable<Transition>)new Transition[0]).GetEnumerator());

            mockTransitionBuilder = new Mock<ITransitionBuilder>();
            mockTransitionBuilder.Setup(m => m.Equals(It.Is<string>(s => s == "builder"))).Returns(true);

            mockCondition = new Mock<Func<bool>>();
            mockCondition.Setup(m => m.Invoke()).Returns(true);

            mockState = new Mock<IState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);

            state = new State(mockOutput.Object, context, mockList.Object, (state, transition) => mockTransitionBuilder.Object);
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
        public void TestDefaultStateSetIfNoTransitionsCreated()
        {
            state.SetDefaultNext(mockState.Object);
            state.MoveNext(default);
            Assert.That(context.State.Equals("mock"));
        }

        [Test]
        public void TestStateSetFromTransition()
        {
            SetupList();
            state.MoveNext(default);
            Assert.That(context.State.Equals("mock"));
        }

        private void SetupList()
        {
            context.Column = 1;
            mockCondition.Setup(m => m.Invoke()).Returns(() => context.Column >= 1);
            var mockEnumerator = new Mock<IEnumerator<Transition>>();
            mockEnumerator.SetupSequence(m => m.MoveNext()).Returns(true).Returns(false);
            mockEnumerator.Setup(m => m.Current).Returns(new Transition()
            {
                Condition = mockCondition.Object,
                NextState = mockState.Object
            });
            mockList.Setup(m => m.GetEnumerator()).Returns(mockEnumerator.Object);
        }
        [Test]
        public void TestStateSetFromTransitionWhenDefaultIsSet()
        {
            SetupList();
            var mockDefault = new Mock<IState>();
            mockDefault.Setup(m => m.Equals(It.Is<string>(s => s == "default"))).Returns(true);
            state.SetDefaultNext(mockDefault.Object);

            state.MoveNext(default);

            Assert.That(context.State.Equals("mock"));
        }

        [Test]
        public void TestColumnIncrementedOnMoveNext()
        {
            state.MoveNext(default);
            Assert.That(context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestColumnZeroed()
        {
            SetupList();
            state.MoveNext(default);
            Assert.That(context.Column, Is.Zero);
        }

        [Test]
        public void TestColumnNotZeroedOnDefaultState()
        {
            state.SetDefaultNext(mockState.Object);
            context.Column = 1;
            state.MoveNext(default);
            Assert.That(context.Column, Is.Not.Zero);
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
            var mockProvier = new Mock<Func<IState, Transition, ITransitionBuilder>>();
            state = new State(mockOutput.Object, context, mockList.Object, mockProvier.Object);

            state.On(mockCondition.Object);

            mockProvier.Verify(m => m.Invoke(It.Is<IState>(s => s.Equals(state)), It.Is<Transition>(t => t.Condition())), Times.Once);
        }

        [Test]
        public void TestCorrectTransitionAddedToList()
        {
            state.On(mockCondition.Object);
            mockList.Verify(m => m.Add(It.Is<Transition>(t => t.Condition())), Times.Once);
        }

        [Test]
        public void TestTransitionSetToNotZeroColumn()
        {
            var transition = new Transition();
            mockList.Setup(m => m.Count).Returns(3);
            mockList.Setup(m => m[2]).Returns(transition);

            state.DoNotZeroColumn();

            Assert.That(transition.ZeroColumn, Is.False);
        }
    }
}
