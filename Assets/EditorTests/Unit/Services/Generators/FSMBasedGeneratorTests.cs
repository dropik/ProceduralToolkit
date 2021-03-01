using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class FSMBasedGeneratorTests : ColumnsBasedGeneratorTests
    {
        private readonly Vector3[] defaultInputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };

        private Mock<IMachine> mockMachine;

        protected Func<int, IMachine> MachineProvider => columns => mockMachine.Object;

        protected override BaseVerticesGenerator CreateGenerator()
        {
            return new FSMBasedGenerator(MachineProvider);
        }

        [SetUp]
        public override void Setup()
        {
            mockMachine = new Mock<IMachine>();
            base.Setup();
        }

        [Test]
        public void TestMachineMoveNextCalledIfHasInput()
        {
            Generator.InputVertices = defaultInputVertices;
            Generator.OutputVertices.Count();
            mockMachine.Verify(m => m.MoveNext(It.IsAny<Vector3>()), Times.Exactly(defaultInputVertices.Length));
        }

        [Test]
        public void TestOutputHasNoNextIfInputIsEmpty()
        {
            Generator.InputVertices = new Vector3[0];
            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
            mockMachine.Verify(m => m.MoveNext(It.IsAny<Vector3>()), Times.Never);
        }

        [Test]
        public void TestOutputReturnsStateValueAsCurrent()
        {
            var expectedVertex = new Vector3(1, 2, 3);
            Generator.InputVertices = defaultInputVertices;
            mockMachine.Setup(m => m.MoveNext(It.Is<Vector3>(v => v == defaultInputVertices[0]))).Returns(new Vector3[] { expectedVertex });

            var enumerator = Generator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current, Is.EqualTo(expectedVertex));
        }
    }
}