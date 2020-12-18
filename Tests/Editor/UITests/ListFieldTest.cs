using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Moq;
using System.Collections.Generic;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldTest : BaseCustomVETest
    {
        private ListField listField;
        private IntegerField sizeField;
        private Mock<IListElementFactory> mockElementFactory;
        private Mock<IList<Object>> mockValueMapper;

        private const string TEST_OBJ_NAME = "Some Object";
        private const string SIZE_FIELD_NAME = "size";
        private const string MOCK_MAPPER_NAME = "Mock Mapper";

        protected override void PreWindowCreation()
        {
            mockElementFactory = new Mock<IListElementFactory>();
            mockValueMapper = new Mock<IList<Object>>();
            mockValueMapper
                .Setup(mock => mock.Equals(It.Is<string>(obj => obj == MOCK_MAPPER_NAME)))
                .Returns(true);
            sizeField = new IntegerField()
            {
                name = SIZE_FIELD_NAME
            };
        }

        protected override VisualElement CreateTestTarget()
        {
            listField = new ListField()
            {
                ElementFactory = mockElementFactory.Object,
                SizeField = sizeField,
                ValueMapper = mockValueMapper.Object
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

        [Test]
        public void TestSizeFieldAddedToHierarchyWhenAssigned()
        {
            var sizeFieldsFound = listField.Query<IntegerField>().ToList();
            Assert.That(sizeFieldsFound.Count, Is.EqualTo(1));
            Assert.That(sizeFieldsFound[0].name, Is.EqualTo(SIZE_FIELD_NAME));
        }

        [Test]
        public void TestAddElementBehaviourWhenNoFactoryAssigned()
        {
            listField.ElementFactory = null;

            try
            {
                sizeField.value++;
                listField.AddElement();
                Assert.Pass();
            }
            catch (System.NullReferenceException)
            {
                Assert.Fail("Exception occured on AddElement apparently due to ElementFactory reference absence.");
            }
        }

        [Test]
        public void TestNewElementIsAddedToHierarchyOnAddElement()
        {
            SetupMockToReturnObjects(1);
            sizeField.value++;
            listField.AddElement();
            mockElementFactory.Verify(
                mock => mock.CreateElement(It.Is<int>(id => id == 0)),
                Times.Once);
            var objectFieldsFound = listField.Query<ObjectField>().ToList();
            Assert.That(objectFieldsFound.Count, Is.EqualTo(1));
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
        public void TestPreviousElementCopiedIfExists()
        {
            SetupMockToReturnObjects(2);
            sizeField.value++;
            listField.AddElement();
            SetupTestObjectOnElementAtId(0);
            sizeField.value++;

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
            for (int i = 0; i < elementsCount; i++)
            {
                sizeField.value++;
                listField.AddElement();
            }
        }

        [Test]
        public void TestReturnedMapperOnValueGet()
        {
            Assert.That(listField.value.Equals(MOCK_MAPPER_NAME));
        }

        [Test]
        public void TestMapperClearedOnValueSet()
        {
            listField.value = new List<Object>();
            mockValueMapper.Verify(mock => mock.Clear(), Times.Once);
        }

        [Test]
        public void TestListCopied()
        {
            const int OBJECTS_TO_CREATE = 2;
            var newList = new List<Object>();
            var testObjects = new Object[OBJECTS_TO_CREATE];
            for (int i = 0; i < OBJECTS_TO_CREATE; i++)
            {
                testObjects[i] = ScriptableObject.CreateInstance<ScriptableObject>();
                testObjects[i].name = TEST_OBJ_NAME;
                newList.Add(testObjects[i]);
            }

            listField.value = newList;
            mockValueMapper.Verify(
                mock => mock.Add(It.Is<Object>(obj => obj.name == TEST_OBJ_NAME)),
                Times.Exactly(OBJECTS_TO_CREATE));
        }
    }
}
