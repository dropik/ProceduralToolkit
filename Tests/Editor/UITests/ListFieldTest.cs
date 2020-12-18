using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Moq;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldTest : BaseCustomVETest
    {
        private ListField listField;
        private IntegerField sizeField;
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
            sizeField = listField.Query<IntegerField>("size").First();

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
        public void TestHasOnlyOneIntegerField()
        {
            var sizeFields = listField.Query<IntegerField>().ToList();
            Assert.That(sizeFields.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestSizeField()
        {
            Assert.That(sizeField, Is.Not.Null);
            Assert.That(sizeField.label, Is.EqualTo("Size"));
            Assert.That(sizeField.value, Is.EqualTo(0));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestCreatedElementsCountWhileIncrementingSizeField(int newSize)
        {
            SetupMockToReturnObjects(newSize);

            sizeField.value = newSize;

            var elements = listField.Query<ObjectField>().ToList();
            Assert.That(elements.Count, Is.EqualTo(newSize));
            mockElementFactory.Verify(mock => mock.CreateElement(It.IsAny<int>()), Times.Exactly(newSize));
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
        [TestCase(1)]
        [TestCase(2)]
        public void TestNewElementWhenShouldBeNull(int elementsToCreate)
        {
            SetupMockToReturnObjects(elementsToCreate);

            sizeField.value = elementsToCreate;

            for (int i = 0; i < elementsToCreate; i++)
            {
                var elementI = listField.Query<ObjectField>($"element{i}").First();
                Assert.That(elementI.value, Is.Null);
            }
        }

        [Test]
        public void TestNewElementWhenShouldNotBeNull()
        {
            SetupMockToReturnObjects(2);
            SetupFirstElement();

            sizeField.value++;

            var element1 = listField.Query<ObjectField>("element1").First();
            Assert.That(element1.value, Is.Not.Null);
            Assert.That(element1.value.name, Is.EqualTo(TEST_OBJ_NAME));
        }

        private void SetupFirstElement()
        {
            sizeField.value = 1;
            var element0 = listField.Query<ObjectField>("element0").First();
            var testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = TEST_OBJ_NAME;
            element0.value = testObject;
        }

        [Test]
        public void TestDecrementingSizeRemovesElementsFromTheEnd()
        {
            SetupMockToReturnObjects(5);
            SetupFirstElement();
            sizeField.value = 5;

            sizeField.value = 3;

            var lastElement = listField.Query<ObjectField>().Last();
            Assert.That(lastElement.name, Is.EqualTo("element2"));
        }

        [Test]
        public void TestSettingNegativeSizeDoesNotChangeValue()
        {
            const int INITIAL_SIZE = 2;
            SetupMockToReturnObjects(INITIAL_SIZE);
            sizeField.value = INITIAL_SIZE;

            sizeField.value = -1;

            Assert.That(sizeField.value, Is.EqualTo(INITIAL_SIZE));
            var elements = listField.Query<ObjectField>().ToList();
            Assert.That(elements.Count, Is.EqualTo(INITIAL_SIZE));
        }

        [Test]
        public void TestValuePropertyUpdatedOnAdd()
        {
            const int VALUES_TO_ADD = 2;
            SetupMockToReturnObjects(VALUES_TO_ADD);
            sizeField.value = VALUES_TO_ADD;

            var value = listField.value;
            Assert.That(value.Count, Is.EqualTo(VALUES_TO_ADD));
        }

        [Test]
        public void TestValuePropertyUpdatedOnRemove()
        {
            const int INITIAL_SIZE = 3;
            const int TARGET_SIZE = 2;
            SetupMockToReturnObjects(INITIAL_SIZE);
            sizeField.value = INITIAL_SIZE;
            sizeField.value = TARGET_SIZE;

            var value = listField.value;
            Assert.That(value.Count, Is.EqualTo(TARGET_SIZE));
        }
    }
}
