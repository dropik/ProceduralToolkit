using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnOriginalTests
    {
        private Vector3[] inputVertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0)
        };
        private DiamondContext context;
        private Mock<IState> mockNextState;
        private ReturnOriginal returnOriginal;

        [SetUp]
        public void Setup()
        {
            context = new DiamondContext(2);
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            returnOriginal = new ReturnOriginal(((IEnumerable<Vector3>)inputVertices).GetEnumerator(), context)
            {
                NextState = mockNextState.Object
            };
        }

        [Test]
        public void TestOnColumnLessThanLength()
        {
            returnOriginal.MoveNext();
            Assert.That(context.Current, Is.EqualTo(inputVertices[0]));
            Assert.That(context.Column, Is.EqualTo(1));
        }
        
        [Test]
        public void TestNextStateDidNotChangeIfColumnNotEnded()
        {
            returnOriginal.MoveNext();
            Assert.That(context.State, Is.Null);
        }

        [Test]
        public void TestNextStateSetIfEndedColumn()
        {
            returnOriginal.MoveNext();
            returnOriginal.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }

        [Test]
        public void TestColumnIsZeroedIfEndedColumn()
        {
            returnOriginal.MoveNext();
            returnOriginal.MoveNext();
            Assert.That(context.Column, Is.Zero);
        }

        [Test]
        public void TestRowIncrementedIfEndedColumn()
        {
            returnOriginal.MoveNext();
            returnOriginal.MoveNext();
            Assert.That(context.Row, Is.EqualTo(1));
        }
    }
}