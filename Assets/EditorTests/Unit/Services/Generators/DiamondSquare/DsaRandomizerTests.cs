using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DsaRandomizerTests : BaseDsaDecoratorTests
    {
        private DsaSettings settings;
        private DsaRandomizer randomizer;
        
        protected override void PreSetup()
        {
            settings = new DsaSettings
            {
                Seed = 123
            };
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new DsaRandomizer(wrappee, settings);

        protected override void PostSetup()
        {
            randomizer = Decorator as DsaRandomizer;
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