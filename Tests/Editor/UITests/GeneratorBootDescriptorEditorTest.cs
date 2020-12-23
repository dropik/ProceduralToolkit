using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class GeneratorBootDescriptorEditorTest
    {
        private GeneratorBootDescriptor descriptor;
        private GeneratorBootDescriptorEditor editor;
        private VisualElement root;

        private const string BASE_SHAPE_NAME = "baseShape";

        private PropertyField BaseShapeField =>
            root.Query<PropertyField>(BASE_SHAPE_NAME).First();

        [SetUp]
        public void SetUp()
        {
            descriptor = ScriptableObject.CreateInstance<GeneratorBootDescriptor>();
            editor = Editor.CreateEditor(descriptor, typeof(GeneratorBootDescriptorEditor))
                        as GeneratorBootDescriptorEditor;
            root = editor.CreateInspectorGUI();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(editor);
            Object.DestroyImmediate(descriptor);
        }

        [Test]
        public void TestHasBaseShapePropertyField()
        {
            Assert.That(BaseShapeField, Is.Not.Null);
        }

        [Test]
        public void TestBaseShapeBindingPath()
        {
            Assert.That(BaseShapeField.bindingPath, Is.EqualTo("baseShape"));
        }
    }
}