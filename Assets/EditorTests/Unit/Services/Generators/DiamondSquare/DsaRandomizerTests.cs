using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DsaRandomizerTests
    {
        private DsaSettings settings;
        private Mock<IDsa> mockWrappee;
        private DsaRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            mockWrappee = new Mock<IDsa>();
            settings = new DsaSettings
            {
                Seed = 123
            };
            randomizer = new DsaRandomizer(mockWrappee.Object, settings);
        }

        [Test]
        public void TestWrappeeExecuted()
        {
            randomizer.Execute();
            mockWrappee.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void TestRandomizer()
        {
            randomizer.Execute();
            var firstValue = Random.Range(0f, 1f);

            Random.InitState(0);
            var secondValue = Random.Range(0f, 1f);            

            randomizer.Execute();
            var thirdValue = Random.Range(0f, 1f);

            Assert.That(firstValue, Is.Not.EqualTo(secondValue));
            Assert.That(firstValue, Is.EqualTo(thirdValue));
        }
    }
}