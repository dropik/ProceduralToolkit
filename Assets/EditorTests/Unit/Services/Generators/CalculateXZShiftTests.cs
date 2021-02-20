using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class CalculateXZShiftTests
    {
        private Vector3[] inputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        private Mock<IState> mockNextState;
        private DiamondContext context;
        private CalculateXZShift calculateShift;

        [SetUp]
        public void Setup()
        {
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            context = new DiamondContext(2)
            {
                First = Vector3.zero
            };
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            calculateShift = new CalculateXZShift(enumerator, context)
            {
                NextState = mockNextState.Object
            };
        }

        [Test]
        public void TestOriginalVertexStored()
        {
            calculateShift.MoveNext();
            Assert.That(context.OriginalVertices[0], Is.EqualTo(inputVertices[0]));
        }

        [Test]
        public void TestNextOriginalVertexStored()
        {
            calculateShift.MoveNext();
            Assert.That(context.OriginalVertices[1], Is.EqualTo(inputVertices[1]));
        }

        [Test]
        public void TestShiftCalculated()
        {
            calculateShift.MoveNext();
            var expectedShift = new Vector3(-2, 0, 0) / 2;
            Assert.That(context.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestDiamondSetAsCurrent()
        {
            calculateShift.MoveNext();
            Assert.That(context.Current, Is.EqualTo(inputVertices[1] + context.XZShift));
        }

        [Test]
        public void TestColumnIncrementedTwice()
        {
            calculateShift.MoveNext();
            Assert.That(context.Column, Is.EqualTo(2));
        }

        [Test]
        public void TestNextStateIsSet()
        {
            calculateShift.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }
    }
}