using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DsaTests
    {
        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 2)]
        [TestCase(9, 3)]
        public void TestStepsExecuted(int length, int expectedCalls)
        {
            var vertices = new Vector3[length * length];
            var context = new LandscapeContext { Vertices = vertices, Length = length };
            var mockDiamondStep = new Mock<IDsaStep>();
            var mockSquareStep = new Mock<IDsaStep>();
            var dsa = new Dsa(context, mockDiamondStep.Object, mockSquareStep.Object);

            dsa.Execute();

            for (int i = 1; i <= expectedCalls; i++)
            {
                mockDiamondStep.Verify(m => m.Execute(i), Times.Once);
                mockSquareStep.Verify(m => m.Execute(i), Times.Once);
            }
        }
    }
}