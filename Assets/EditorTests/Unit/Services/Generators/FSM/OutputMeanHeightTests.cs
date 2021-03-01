using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputMeanHeightTests
    {
        [Test]
        public void TestMeanHeightOutput()
        {
            var input = new Vector3(1, 2, 3);
            var height = 10f;
            var expected = new Vector3(1, 3, 3);
            var context = new AdderContext(3);
            context.Heights.Enqueue(height);
            var output = new OutputMeanHeight(context);

            var result = output.GetOutputFor(input).First();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
