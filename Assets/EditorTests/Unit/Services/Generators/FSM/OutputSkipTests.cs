using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputSkipTests
    {
        [Test]
        public void TestMoveNextReturnsEmpty()
        {
            var expectedVertices = new Vector3[0];
            var output = new OutputSkip();
            CollectionAssert.AreEqual(expectedVertices, output.GetOutputFor(default));
        }
    }
}