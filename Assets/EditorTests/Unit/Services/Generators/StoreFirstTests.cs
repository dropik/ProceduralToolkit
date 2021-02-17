using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class StoreFirstTests
    {
        private Vector3[] inputVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0)
        };
        private DiamondContext context;
        private Mock<IState> mockNextState;
        private StoreFirst storeFirst;

        [SetUp]
        public void Setup()
        {
            mockNextState = new Mock<IState>();
            mockNextState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
            context = new DiamondContext(2);
            var enumerator = ((IEnumerable<Vector3>)inputVertices).GetEnumerator();
            storeFirst = new StoreFirst(enumerator, context)
            {
                NextState = mockNextState.Object
            };
        }

        [Test]
        public void TestCurrentSetAsInputVertex()
        {
            storeFirst.MoveNext();
            Assert.That(context.Current, Is.EqualTo(inputVertices[0]));
        }

        [Test]
        public void TestNextStateSet()
        {
            storeFirst.MoveNext();
            Assert.That(context.State.Equals("mock"));
        }
    }
}
