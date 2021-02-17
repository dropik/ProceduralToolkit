using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class SkipAndReturnDiamondTests
    {
        private Vector3[] inputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        private Mock<IState> mockNextState;
        private DiamondContext context;
        private SkipAndReturnDiamond skip;

        [SetUp]
        public void Setup()
        {
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            context = new DiamondContext(2)
            {
                XZShift = new Vector3(0.5f, 0, 0.5f)
            };
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            skip = new SkipAndReturnDiamond(enumerator, context)
            {
                NextState = mockNextState.Object
            };
        }

        [Test]
        public void TestOriginalVertexStored()
        {
            skip.MoveNext();
            Assert.That(context.OriginalVertices[0], Is.EqualTo(inputVertices[0]));
        }

        [Test]
        public void TestNextOriginalVertexStored()
        {
            skip.MoveNext();
            Assert.That(context.OriginalVertices[1], Is.EqualTo(inputVertices[1]));
        }

        [Test]
        public void TestDiamondSetAsCurrent()
        {
            skip.MoveNext();
            Assert.That(context.Current, Is.EqualTo(inputVertices[1] + context.XZShift));
        }

        [Test]
        public void TestColumnIncrementedTwice()
        {
            skip.MoveNext();
            Assert.That(context.Column, Is.EqualTo(2));
        }

        [Test]
        public void TestNextStateIsSet()
        {
            skip.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }
    }
}