using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Models;
using System;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components.Generators
{
    [Category("Unit")]
    public class DiamondSquareTests
    {
        private DiamondSquare ds;

        [SetUp]
        public void Setup()
        {
            ds = new GameObject().AddComponent<DiamondSquare>();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(ds.gameObject);
        }

        private void SetupValues()
        {
            ds.seed = 123;
            ds.iterations = 0;
            ds.magnitude = 10;
            ds.hardness = 0;
        }

        [Test]
        public void TestResetValues()
        {
            SetupValues();

            ds.Reset();

            Assert.That(ds.seed, Is.Zero);
            Assert.That(ds.iterations, Is.EqualTo(2));
            Assert.That(ds.magnitude, Is.EqualTo(1));
            Assert.That(ds.hardness, Is.EqualTo(0.5f));
        }

        [Test]
        public void TestOnValidateInvokesUpdatedEvent()
        {
            var mockAction = new Mock<Action>();
            ds.Updated += mockAction.Object;

            ds.OnValidate();

            mockAction.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestSettingsCreated()
        {
            SetupValues();
            var expectedSettings = new DSASettings { Seed = 123, Iterations = 0, Magnitude = 10, Hardness = 0 };
            Assert.That(ds.Settings, Is.EqualTo(expectedSettings));
        }
    }
}
