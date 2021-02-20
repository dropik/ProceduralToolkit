using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnOriginalTests : BaseReturnVertexTests
    {
        private Mock<IState> mockState2;

        protected override BaseReturnVertex GetReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            return new ReturnOriginal(inputVerticesEnumerator, context);
        }

        [Test]
        public void TestCurrentSetAsGivenVertex()
        {
            ReturnVertex.MoveNext();
            Assert.That(Context.Current, Is.EqualTo(InputVertices[0]));
        }

        [Test]
        public void TestMoveNextReturnsCurrent()
        {
            var returnOriginal = ReturnVertex as ReturnOriginal;
            Assert.That(returnOriginal.MoveNext(InputVertices[0]), Is.EqualTo(InputVertices[0]));
        }

        protected override BaseReturnVertex GetReturnVertex(DiamondContext context)
        {
            return new ReturnOriginal(context);
        }
    }
}