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
                ElementFactory = mockElementFactory.Object
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
            SetupTestObjectOnElementAtId(0);

            listField.AddElement();

            var element1 = listField.Query<ObjectField>("element1").First();
            Assert.That(element1.value.name, Is.EqualTo(TEST_OBJ_NAME));
        }

        private void SetupTestObjectOnElementAtId(int id)
        {
            var testObject = CreateTestObject();
            AssignObjectToElementAtId(testObject, id);
        }

        private Object CreateTestObject()
        {
            var testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
            return testObject;
        }

        private void AssignObjectToElementAtId(Object testObject, int id)
        {
            var element = listField.Query<ObjectField>($"element{id}").First();
            element.value = testObject;
        }

        [Test]
        public void TestCorrectObjectFieldRemovedOnRemoveElement()
        {
            CreateEmptyElements(2);

            listField.RemoveElement();

            var foundElements = listField.Query<ObjectField>().ToList();
            Assert.That(foundElements.Count, Is.EqualTo(1));
            Assert.That(foundElements[0].name, Is.EqualTo("element0"));
        }

        private void CreateEmptyElements(int elementsCount)
        {
            SetupMockToReturnObjects(elementsCount);
            listField.AddElement();
            listField.AddElement();
        }

        [Test]
        public void TestLastValueRemovedOnRemoveElement()
        {
            const int INITIAL_OBJECTS_COUNT = 2;
            SetupMockToReturnObjects(2);
            SetupListElements(INITIAL_OBJECTS_COUNT);

            listField.RemoveElement();

            Assert.That(listField.value.Count, Is.EqualTo(1));
            Assert.That(listField.value[0].name, Is.EqualTo($"{TEST_OBJ_NAME}0"));
        }

        private void SetupListElements(int elementsCount)
        {
            for (int i = 0; i < elementsCount; i++)
            {
                listField.AddElement();
                SetTestElementAtId(i);
            }
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
            const int TEST_ID = 1;
            CreateEmptyElements(2);
            SetupTestObjectOnElementAtId(TEST_ID);

            listField.UpdateValueAt(TEST_ID);

            Assert.That(listField.value[TEST_ID].name, Is.EqualTo(TEST_OBJ_NAME));
        }

        [Test]
        public void TestAddElementBehaviourWhenNoFactoryAssigned()
        {
            listField.ElementFactory = null;

            try
            {
                listField.AddElement();
                Assert.Pass();
            }
            catch (System.NullReferenceException)
            {
                Assert.Fail("Exception occured on AddElement apparently due to ElementFactory reference absence.");
            }
        }

        [Test]
        public void TestSizeFieldAddedToHierarchyWhenAssigned()
        {
            var sizeField = new IntegerField
            {
                name = "size"
            };

            listField.SizeField = sizeField;

            var sizeFieldsFound = listField.Query<IntegerField>().ToList();

            Assert.That(sizeFieldsFound.Count, Is.EqualTo(1));
            Assert.That(sizeFieldsFound[0].name, Is.EqualTo("size"));
        }
    }
}
