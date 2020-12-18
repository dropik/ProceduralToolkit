using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Moq;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldTest : BaseCustomVETest
    {
        private ListField listField;
        private Mock<IListElementFactory> mockElementFactory;

        private const string TEST_OBJ_NAME = "Some Object";

        protected override void PreWindowCreation()
        {
            mockElementFactory = new Mock<IListElementFactory>();
        }

        protected override VisualElement CreateTestTarget()
        {
            listField = new ListField()
            {
                ElementFactory = mockElementFactory.Object,
                ObjectType = typeof(ScriptableObject)
            };

            return listField;
        }

        [Test]
        public void TestHasOnlyOneFoldout()
        {
            var foldouts = listField.Query<Foldout>().ToList();
            Assert.That(foldouts.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestFoldoutIsFold()
        {
            var foldout = listField.Query<Foldout>().First();
            Assert.That(foldout.value, Is.False);
        }

        private void SetupMockToReturnObjects(int objectsToCreate)
        {
            for (int i = 0; i < objectsToCreate; i++)
            {
                mockElementFactory
                    .Setup(mock => mock.CreateElement(i))
                    .Returns(new ObjectField() { name = $"element{i}" });
            }
        }

        [Test]
        public void TestNewValueIsAddedToListOnAddElement()
        {
            listField.AddElement();
            Assert.That(listField.value.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestNewElementIsAddedToHierarchyOnAddElement()
        {
            SetupMockToReturnObjects(1);
            listField.AddElement();
            mockElementFactory.Verify(
                mock => mock.CreateElement(It.Is<int>(id => id == 0)),
                Times.Once);
            var objectFieldsFound = listField.Query<ObjectField>().ToList();
            Assert.That(objectFieldsFound.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestPreviousElementCopiedIfExists()
        {
            SetupMockToReturnObjects(2);
            listField.AddElement();
            var element0 = listField.Query<ObjectField>("element0").First();
            var testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
            element0.value = testObject;

            listField.AddElement();

            var element1 = listField.Query<ObjectField>("element1").First();
            Assert.That(element1.value.name, Is.EqualTo(TEST_OBJ_NAME));
        }

        [Test]
        public void TestCorrectObjectFieldRemovedOnRemoveElement()
        {
            SetupMockToReturnObjects(2);
            listField.AddElement();
            listField.AddElement();

            listField.RemoveElement();

            var foundElements = listField.Query<ObjectField>().ToList();
            Assert.That(foundElements.Count, Is.EqualTo(1));
            Assert.That(foundElements[0].name, Is.EqualTo("element0"));
        }

        [Test]
        public void TestLastValueRemovedOnRemoveElement()
        {
            const int INITIAL_OBJECTS_COUNT = 2;
            SetupMockToReturnObjects(2);
            for (int i = 0; i < INITIAL_OBJECTS_COUNT; i++)
            {
                listField.AddElement();
                SetTestElementAtId(i);
            }

            listField.RemoveElement();

            Assert.That(listField.value.Count, Is.EqualTo(1));
            Assert.That(listField.value[0].name, Is.EqualTo($"{TEST_OBJ_NAME}0"));
        }

        private void SetTestElementAtId(int id)
        {
            var testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = $"{TEST_OBJ_NAME}{id}";
            listField.value[id] = testObject;
        }

        [Test]
        public void TestCorrectListValueUpdatedOnUpdateValueAt()
        {
            SetupMockToReturnObjects(2);
            listField.AddElement();
            listField.AddElement();
            var testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
            var element1 = listField.Query<ObjectField>("element1").First();
            element1.value = testObject;

            listField.UpdateValueAt(1);

            Assert.That(listField.value[1].name, Is.EqualTo(TEST_OBJ_NAME));
        }
    }
}
