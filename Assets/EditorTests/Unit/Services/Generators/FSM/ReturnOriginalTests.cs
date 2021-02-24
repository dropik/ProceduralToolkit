using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnOriginalTests : BaseStateTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new FSMContext(columns);
        }

        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new ReturnOriginal(settings);
        }

        [Test]
        public void TestMoveNextReturnsCurrent()
        {
            var expectedVertices = new Vector3[] { InputVertices[0] };
            CollectionAssert.AreEqual(expectedVertices, ReturnVertex.MoveNext(InputVertices[0]));
        }
    }
}