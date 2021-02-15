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
        [Test]
        public void TestOnColumnLessThanLength()
        {
            var inputVertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0)
            };
            var context = new DiamondContext()
            {
                Length = 3
            };
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            var returnOriginal = new ReturnOriginal(enumerator, context, null);
            returnOriginal.MoveNext();
            Assert.That(context.Current, Is.EqualTo(inputVertices[0]));
            Assert.That(context.Column, Is.EqualTo(1));
        }

        [Test]
        public void TestNextStateSetOnColumnReachedLength()
        {
            var inputVertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0)
            };
            var context = new DiamondContext()
            {
                Length = 2,
            };
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            var mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            var returnOriginal = new ReturnOriginal(enumerator, context, mockNextState.Object);
            returnOriginal.MoveNext();
            Assert.That(context.State, Is.Null);
            returnOriginal.MoveNext();
            Assert.That(context.State.Equals("mock"));
            Assert.That(context.Current, Is.EqualTo(inputVertices[1]));
            Assert.That(context.Column, Is.Zero);
        }
    }
}