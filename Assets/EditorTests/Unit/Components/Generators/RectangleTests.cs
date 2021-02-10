using System;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components.Generators
{
    [Category("Unit")]
    public class RectangleTests
    {
        private GameObject gameObject;
        private Rectangle rect;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            rect = gameObject.AddComponent<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestResetValues()
        {
            Assert.That(rect.length, Is.EqualTo(1));
            Assert.That(rect.width, Is.EqualTo(1));
        }

        [Test]
        public void TestLengthMinValue()
        {
            TestMinValueForField("length");
        }

        [Test]
        public void TestWidthMinValue()
        {
            TestMinValueForField("width");
        }

        private void TestMinValueForField(string fieldName)
        {
            var type = rect.GetType();
            var lengthField = type.GetField(fieldName);
            var minAttribute = (MinAttribute)Attribute.GetCustomAttribute(lengthField, typeof(MinAttribute));
            Assert.That(minAttribute.min, Is.Zero);
        }

        [Test]
        public void TestUpdatedEventInvoked()
        {
            var mockAction = new Mock<Action>();
            rect.Updated += mockAction.Object;
            rect.OnValidate();
            mockAction.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestRectangleSettingsCreated()
        {
            const int testLength = 10;
            const int testWidth = 15;
            rect.length = testLength;
            rect.width = testWidth;
            var settings = rect.Settings;
            var expectedSettings = new RectangleGeneratorSettings()
            {
                Length = testLength,
                Width = testWidth
            };
            Assert.That(settings, Is.EqualTo(expectedSettings));
        }
    }
}