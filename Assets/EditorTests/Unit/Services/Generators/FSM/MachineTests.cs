using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class MachineTests
    {
        private Machine machine;
        private Mock<Action<IStateBuilder>> mockBuild;
        private Mock<IStateBuilder> mockStateBuilder;
        private Mock<IDictionary<string, IState>> mockDictionary;
        private Mock<IState> mockState;

        [SetUp]
        public void Setup()
        {
            mockBuild = new Mock<Action<IStateBuilder>>();

            mockStateBuilder = new Mock<IStateBuilder>();
            mockStateBuilder.Setup(m => m.Equals("builder")).Returns(true);
            mockStateBuilder.Setup(m => m.BuildState(It.Is<IMachine>(machine => machine.Equals(machine)))).Returns(() => mockState.Object);

            mockDictionary = new Mock<IDictionary<string, IState>>();

            mockState = new Mock<IState>();
            mockState.Setup(m => m.Equals("state")).Returns(true);

            machine = new Machine(mockDictionary.Object, () => mockStateBuilder.Object);
        }

        [Test]
        public void TestOnAddEntryAddedToDictionary()
        {
            const string name = "state";
            machine.AddState(name, default);
            mockDictionary.Verify(m => m.Add(name, It.IsAny<IState>()), Times.Once);
        }

        [Test]
        public void TestOnAddBuilderActionCalled()
        {
            machine.AddState(default, mockBuild.Object);
            mockBuild.Verify(m => m.Invoke(It.Is<IStateBuilder>(b => b.Equals("builder"))), Times.Once);
        }

        [Test]
        public void TestOnAddStateBuiltUsingBuilder()
        {
            machine.AddState(default, default);
            mockStateBuilder.Verify(m => m.BuildState(It.IsAny<IMachine>()), Times.Once);
        }

        [Test]
        public void TestOnAddBuiltStateAddedToDictionary()
        {
            machine.AddState(default, default);
            mockDictionary.Verify(m => m.Add(It.IsAny<string>(), It.Is<IState>(s => s.Equals("state"))), Times.Once);
        }

        [Test]
        public void TestOnSetStateDictionaryUsed()
        {
            const string name = "state";
            machine.SetState(name);
            mockDictionary.Verify(m => m[name], Times.Once);
        }

        [Test]
        public void TestOnMoveNextCurrentStateUsed()
        {
            const string name = "state";
            mockDictionary.Setup(m => m[name]).Returns(mockState.Object);
            var expectedVertices = new Vector3[]
            {
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0),
                new Vector3(3, 0, 0)
            };
            var input = new Vector3(1, 2, 3);
            mockState.Setup(m => m.MoveNext(input)).Returns(expectedVertices);

            machine.SetState(name);
            var result = machine.MoveNext(input);

            mockState.Verify(m => m.MoveNext(input), Times.Once);
            CollectionAssert.AreEqual(expectedVertices, result);
        }

        [Test]
        public void TestOnMoveNextWithNullCurrentStateReturnsEmptyArray()
        {
            CollectionAssert.AreEqual(new Vector3[0], machine.MoveNext(default));
        }
    }
}