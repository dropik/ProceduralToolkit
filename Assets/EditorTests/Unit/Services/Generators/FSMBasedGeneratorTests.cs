using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
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

        private Mock<IStateBehaviour> mockState;
        private FSMContext context;

        protected override Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider => (vertices, columns) => context;

        protected override BaseVerticesGenerator CreateGenerator()
        {
            return new FSMBasedGenerator(ContextProvider);
        }

        [SetUp]
        public override void Setup()
        {
            mockState = new Mock<IStateBehaviour>();
            context = new FSMContext(2)
            {
                State = mockState.Object
            };
            base.Setup();
        }

        [Test]
        public void TestOutputHasNextIfInputIsNotEmpty()
        {
            Generator.InputVertices = defaultInputVertices;
            mockState.Setup(m => m.MoveNext(It.IsAny<Vector3>())).Returns(null as IEnumerable<Vector3>);
            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.True);
        }

        [Test]
        public void TestOutputReturnsInputIfStateIsNull()
        {
            Generator.InputVertices = defaultInputVertices;
            context.State = null;

            var enumerator = Generator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current, Is.EqualTo(defaultInputVertices[0]));
        }

        [Test]
        public void TestOutputHasNoNextIfInputIsEmpty()
        {
            Generator.InputVertices = new Vector3[0];
            mockState.Setup(m => m.MoveNext(It.IsAny<Vector3>())).Returns(new Vector3[] { Vector3.zero });
            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
            mockState.Verify(m => m.MoveNext(It.IsAny<Vector3>()), Times.Never);
        }

        [Test]
        public void TestOutputReturnsStateValueAsCurrent()
        {
            var expectedVertex = new Vector3(1, 2, 3);
            Generator.InputVertices = defaultInputVertices;
            mockState.Setup(m => m.MoveNext(It.Is<Vector3>(v => v == defaultInputVertices[0]))).Returns(new Vector3[] { expectedVertex });

            var enumerator = Generator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.Current, Is.EqualTo(expectedVertex));
        }
    }
}