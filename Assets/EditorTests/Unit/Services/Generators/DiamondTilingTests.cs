using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DiamondTilingTests : BaseDiamondGeneratorTests
    {
        private readonly Vector3[] defaultInputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };

        private Mock<IDiamondTilingState> mockState;
        private FSMContext context;

        protected override Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider => (vertices, columns) => context;

        protected override BaseDiamondGenerator CreateGenerator()
        {
            return new DiamondTiling(ContextProvider);
        }

        [SetUp]
        public override void Setup()
        {
            mockState = new Mock<IDiamondTilingState>();
            context = new FSMContext(2)
            {
                State = mockState.Object
            };
            base.Setup();
        }

        [Test]
        public void TestOutputHasNextIfStateHasNext()
        {
            Generator.InputVertices = defaultInputVertices;
            mockState.Setup(m => m.MoveNext(It.Is<Vector3>(v => v == defaultInputVertices[0]))).Returns(Vector3.zero);

            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.True);
        }

        [Test]
        public void TestOutputHasNotNextIfStateHasNotNext()
        {
            Generator.InputVertices = defaultInputVertices;
            mockState.Setup(m => m.MoveNext(It.IsAny<Vector3>())).Returns(null as Vector3?);

            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
        }

        [Test]
        public void TestOutputHasNotNextIfStateIsNull()
        {
            Generator.InputVertices = defaultInputVertices;
            context.State = null;

            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
        }

        [Test]
        public void TestOutputHasNoNextIfInputIsEmpty()
        {
            Generator.InputVertices = new Vector3[0];
            mockState.Setup(m => m.MoveNext(It.IsAny<Vector3>())).Returns(Vector3.zero);
            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
            mockState.Verify(m => m.MoveNext(It.IsAny<Vector3>()), Times.Never);
        }

        [Test]
        public void TestOutputReturnsStateValueAsCurrent()
        {
            var expectedVertex = new Vector3(1, 2, 3);
            Generator.InputVertices = defaultInputVertices;
            mockState.Setup(m => m.MoveNext(It.Is<Vector3>(v => v == defaultInputVertices[0]))).Returns(expectedVertex);

            var enumerator = Generator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current, Is.EqualTo(expectedVertex));
        }
    }
}