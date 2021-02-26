using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class TransitionBehaviourTests
    {
        private FSMContext context;
        private Mock<IMachine> mockMachine;

        [SetUp]
        public void Setup()
        {
            context = new FSMContext();
            mockMachine = new Mock<IMachine>();
        }

        [Test]
        public void TestWithOneFalseConditionedTransition()
        {
            const string name = "state";
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => false,
                    NextState = name
                }
            };
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();
            mockMachine.Verify(m => m.SetState(name), Times.Never);
        }

        [Test]
        public void TestWithOneTrueConditionedTransition()
        {
            const string name = "state";
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => true,
                    NextState = name
                }
            };
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();
            mockMachine.Verify(m => m.SetState(name), Times.Once);
        }

        [Test]
        public void TestWithFirstTrueSecondFalse()
        {
            const string name1 = "state1";
            const string name2 = "state2";
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => true,
                    NextState = name1
                },
                new Transition()
                {
                    Condition = () => false,
                    NextState = name2
                }
            };
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();
            mockMachine.Verify(m => m.SetState(name1), Times.Once);
            mockMachine.Verify(m => m.SetState(name2), Times.Never);
        }

        [Test]
        public void TestWithFirstFalseSecondTrue()
        {
            const string name1 = "state1";
            const string name2 = "state2";
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => false,
                    NextState = name1
                },
                new Transition()
                {
                    Condition = () => true,
                    NextState = name2
                }
            };
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();
            mockMachine.Verify(m => m.SetState(name1), Times.Never);
            mockMachine.Verify(m => m.SetState(name2), Times.Once);
        }

        [Test]
        public void TestWithConditionUsingColumn()
        {
            const string name1 = "state1";
            const string name2 = "state2";
            context.Column = 1;
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => context.Column >= 2,
                    NextState = name1
                },
                new Transition()
                {
                    Condition = () => true,
                    NextState = name2
                }
            };
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();
            mockMachine.Verify(m => m.SetState(name1), Times.Once);
            mockMachine.Verify(m => m.SetState(name2), Times.Never);
        }

        [Test]
        public void TestColumnIncremented()
        {
            var transitionBehaviour = new TransitionBehaviour(context, new Transition[0], mockMachine.Object);
            transitionBehaviour.Execute();
            Assert.That(context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestColumnZeroed()
        {
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => true
                }
            };
            context.Column = 1;
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();

            Assert.That(context.Column, Is.Zero);
        }

        [Test]
        public void TestColumnNotZeroedIfPropertySet()
        {
            var transitions = new Transition[]
            {
                new Transition()
                {
                    Condition = () => true,
                    ZeroColumn = false
                }
            };
            context.Column = 1;
            var transitionBehaviour = new TransitionBehaviour(context, transitions, mockMachine.Object);

            transitionBehaviour.Execute();

            Assert.That(context.Column, Is.Not.Zero);
        }
    }
}
