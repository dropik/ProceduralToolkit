using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputAddedHeightTests
    {
        [Test]
        public void TestHeightAddedToOutput()
        {
            var input = new Vector3(1, 2, 3);
            var height = 10;
            var context = new AdderContext(3);
            context.Heights.Enqueue(height);
            var expectedVertex = new Vector3(input.x, input.y + height, input.z);
            var output = new OutputAddedHeight(context);

            var result = output.GetOutputFor(input).First();

            Assert.That(result, Is.EqualTo(expectedVertex));
        }
    }
}
