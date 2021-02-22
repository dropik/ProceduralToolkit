using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class SkipVertexTests : BaseStateTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new FSMContext(columns);
        }

        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new SkipVertex(settings);
        }

        [Test]
        public void TestMoveNextReturnsEmpty()
        {
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, ReturnVertex.MoveNext(InputVertices[0]));
        }
    }
}