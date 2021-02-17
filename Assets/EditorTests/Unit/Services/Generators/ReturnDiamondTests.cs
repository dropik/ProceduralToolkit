using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnDiamondTests
    {
        private Vector3[] inputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        private DiamondContext context;
        private Mock<IState> mockNextState;
        private ReturnDiamond returnDiamond;

        [SetUp]
        public void Setup()
        {
            context = new DiamondContext(2)
            {
                XZShift = new Vector3(0.5f, 0, 0.5f)
            };
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            returnDiamond = new ReturnDiamond(enumerator, context)
            {
                NextState = mockNextState.Object
            };
        }

        [Test]
        public void TestOriginalVertexIsStored()
        {
            returnDiamond.MoveNext();
            Assert.That(context.OriginalVertices[0], Is.EqualTo(inputVertices[0]));
        }
        
        [Test]
        public void TestDiamondCalculatedCorrectly()
        {
            var expectedVertex = inputVertices[0] + context.XZShift;
            returnDiamond.MoveNext();
            Assert.That(context.Current, Is.EqualTo(expectedVertex));
        }

        [Test]
        public void TestNextStateDidNotChangeIfNotEndedColumn()
        {
            returnDiamond.MoveNext();
            Assert.That(context.State, Is.Null);
        }

        [Test]
        public void TestNextStateSetIfEndedColumn()
        {
            returnDiamond.MoveNext();
            returnDiamond.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }

        [Test]
        public void TestRowIncrementedIfEndedColumn()
        {
            returnDiamond.MoveNext();
            returnDiamond.MoveNext();
            Assert.That(context.Row, Is.EqualTo(1));
        }
    }
}
