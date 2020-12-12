using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEngine;
using Moq;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListElementFieldTest
    {
        private ListElementField field;
        private EditorWindow window;
        private Mock<IListField> mockListField;
        private ScriptableObject testObject;

        private const int TEST_ID = 2;
        private const string TEST_OBJ_NAME = "Test Object";
        private System.Type TestType => typeof(ScriptableObject);

        [SetUp]
        public void SetUp()
        {
            SetupMockList();
            CreateElement();
            InitWindow();
            CreateTestObject();
        }

        private void SetupMockList()
        {
            mockListField = new Mock<IListField>();
            mockListField.SetupGet(m => m.ObjectType).Returns(TestType);
        }

        private void CreateElement()
        {
            field = new ListElementField(mockListField.Object, TEST_ID);
        }

        private void InitWindow()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            window.rootVisualElement.Add(field);
        }

        private void CreateTestObject()
        {
            testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }

        [Test]
        public void TestElementProperties()
        {
            Assert.That(field.name, Is.EqualTo($"element{TEST_ID}"));
            Assert.That(field.label, Is.EqualTo($"Element {TEST_ID}"));
            Assert.That(field.objectType, Is.EqualTo(TestType));
        }

        [Test]
        public void TestUpdatingElementTheListGetsUpdated()
        {
            field.value = testObject;
            mockListField.Verify(mock => mock.UpdateValueAt(TEST_ID), Times.Once);
        }
    }
}
