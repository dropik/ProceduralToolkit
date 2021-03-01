using NUnit.Framework;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class IterationToLengthTests
    {
        [Test]
        [TestCase(0, 2)]
        [TestCase(2, 5)]
        public void TestCalculatedLength(int iteration, int expectedLength)
        {
            var iterationToLength = new IterationToLength()
            {
                Iteration = iteration
            };
            Assert.That(iterationToLength.Length, Is.EqualTo(expectedLength));
        }
    }
}
