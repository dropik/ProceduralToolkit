using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEngine;
using Moq;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListElementFieldTest : BaseCustomVETest
    {
        private ListElementField field;
        private Mock<IListField> mockListField;
        private ScriptableObject testObject;

        private const int TEST_ID = 2;
        private const string TEST_OBJ_NAME = "Test Object";
        private System.Type TestType => typeof(ScriptableObject);

        protected override void PreWindowCreation()
        {
            SetupMockList();
            CreateTestObject();
        }

        private void SetupMockList()
        {
            mockListField = new Mock<IListField>();
            mockListField.SetupGet(m => m.ObjectType).Returns(TestType);
        }

        private void CreateTestObject()
        {
            testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
        }

        protected override VisualElement CreateTestTarget()
        {
            field = new ListElementField(mockListField.Object, TEST_ID);
            return field;
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
