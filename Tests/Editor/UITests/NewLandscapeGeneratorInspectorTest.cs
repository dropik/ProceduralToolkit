using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class NewLandscapeGeneratorInspectorTest
    {
        private NewLandscapeGeneratorWindow window;
        private InspectorElement inspector;

        private const string BASE_SHAPE_FIELD_NAME = "baseShape";

        [SetUp]
        public void SetUp()
        {
            window = EditorWindow.CreateWindow<NewLandscapeGeneratorWindow>();
            inspector = new InspectorElement(window);
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }

        [Test]
        public void TestHasBaseShapeField()
        {
            var baseShapeField = inspector.Query<PropertyField>(BASE_SHAPE_FIELD_NAME).First();
            Assert.That(baseShapeField, Is.Not.Null);
            Assert.That(baseShapeField.bindingPath, Is.EqualTo(BASE_SHAPE_FIELD_NAME));
        }
    }
}
