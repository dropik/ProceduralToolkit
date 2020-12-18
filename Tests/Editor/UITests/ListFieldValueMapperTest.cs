using NUnit.Framework;
using UnityEditor.UIElements;
using ProceduralToolkit.UI;
using Moq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldValueMapperTest
    {
        private ListFieldValueMapper mapper;
        private Mock<IListField> listField;
        private IntegerField sizeField;
        private ObjectField element0;
        private Object testObject;

        private const int SIZE = 2;
        private const string TEST_OBJ_NAME = "Test Object";

        [SetUp]
        public void SetUp()
        {
            sizeField = new IntegerField();
            listField = new Mock<IListField>();
            listField.SetupGet(mock => mock.SizeField).Returns(sizeField);
            mapper = new ListFieldValueMapper(listField.Object);
        }

        [Test]
        public void TestCountGivesSizeFieldValue()
        {
            sizeField.value = SIZE;
            var size = mapper.Count;
            listField.VerifyGet(mock => mock.SizeField, Times.Once);
            Assert.That(size, Is.EqualTo(SIZE));
        }

        [Test]
        public void TestIndexingGet()
        {
            SetupForIndexing();
            var indexedValue = mapper[0];
            Assert.That(indexedValue.name, Is.EqualTo(TEST_OBJ_NAME));
        }

        private void SetupForIndexing()
        {
            var root = new VisualElement();
            element0 = CreateElementWithId(0);
            root.Add(element0);
            listField.SetupGet(mock => mock.ElementsRoot).Returns(root);
        }

        private ObjectField CreateElementWithId(int id)
        {
            CreateTestObjectWithName(TEST_OBJ_NAME);
            return new ObjectField()
            {
                name = $"element{id}",
                value = testObject
            };
        }

        private void CreateTestObjectWithName(string name)
        {
            testObject = ScriptableObject.CreateInstance<ScriptableObject>();
            testObject.name = name;
        }

        [Test]
        public void TestIndexingSet()
        {
            const string SET_OBJ_NAME = "Object to Set";
            SetupForIndexing();
            var objectToSet = ScriptableObject.CreateInstance<ScriptableObject>();
            objectToSet.name = SET_OBJ_NAME;

            mapper[0] = objectToSet;

            Assert.That(element0.value.name, Is.EqualTo(SET_OBJ_NAME));
        }

        [Test]
        public void TestIsReadOnlyFalse()
        {
            Assert.That(mapper.IsReadOnly, Is.False);
        }

        [Test]
        public void TestAddBehaviour()
        {
            const string ADD_OBJ_NAME = "Added Object";
            SetupForIndexing();
            CreateTestObjectWithName(ADD_OBJ_NAME);

            mapper.Add(testObject);

            Assert.That(sizeField.value, Is.EqualTo(1));
            Assert.That(element0.value.name, Is.EqualTo(ADD_OBJ_NAME));
        }

        [Test]
        public void TestSizeFieldZeroedOnClear()
        {
            sizeField.value = SIZE;
            mapper.Clear();
            Assert.That(sizeField.value, Is.Zero);
        }

        [Test]
        public void TestContainsWhenShouldNotBeFound()
        {
            SetupForIndexing();
            sizeField.value = 1;
            CreateTestObjectWithName("Missing Object");
            Assert.That(mapper.Contains(testObject), Is.False);
        }

        [Test]
        public void TestContainsWhenShouldBeFound()
        {
            SetupForIndexing();
            sizeField.value = 1;
            Assert.That(mapper.Contains(testObject), Is.True);
        }
    }
}
