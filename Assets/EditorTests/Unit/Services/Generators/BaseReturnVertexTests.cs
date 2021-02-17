using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class BaseReturnVertexTests
    {
        protected Vector3[] InputVertices => new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        protected DiamondContext Context { get; private set; }
        private Mock<IState> mockNextState;
        protected BaseReturnVertex ReturnVertex { get; private set; }

        [SetUp]
        public void Setup()
        {
            Context = new DiamondContext(2)
            {
                XZShift = new Vector3(0.5f, 0, 0.5f)
            };
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            var enumerator = ((IEnumerable<Vector3>)InputVertices).GetEnumerator();
            ReturnVertex = GetReturnVertex(enumerator, Context);
            ReturnVertex.NextState = mockNextState.Object;
        }

        protected abstract BaseReturnVertex GetReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context);

        [Test]
        public void TestNextStateDidNotChangeIfNotEndedColumn()
        {
            ReturnVertex.MoveNext();
            Assert.That(Context.State, Is.Null);
        }

        [Test]
        public void TestNextStateSetIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.State.Equals("mock"));
        }

        [Test]
        public void TestColumnZeroedIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.Column, Is.Zero);
        }

        [Test]
        public void TestRowIncrementedIfEndedColumn()
        {
            ReturnVertex.MoveNext();
            ReturnVertex.MoveNext();
            Assert.That(Context.Row, Is.EqualTo(1));
        }
    }
}
