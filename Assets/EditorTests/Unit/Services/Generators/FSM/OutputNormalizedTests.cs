using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputNormalizedTests
    {
        [Test]
        public void TestOutputIsNormalized()
        {
            var input = new Vector3(4, 5, 6);
            var expected = new Vector3(4, 1.25f, 6);
            var output = new OutputNormalized(4);

            var result = output.GetOutputFor(input).First();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
