using NUnit.Framework;
using UnityEditor.UIElements;
using ProceduralToolkit.UI;
using Moq;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldValueMapperTest
    {
        private IList<Object> mapper;
        private Mock<IListField> listField;
        private IntegerField sizeField;
        private ObjectField[] elements;
        private Object testObject;

        private const int SIZE = 2;
        private const string TEST_OBJ_NAME = "Test Object";
        private const string MISSING_OBJ_NAME = "Missing Object";

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
            SetupForIndexing(1);
            var indexedValue = mapper[0];
            Assert.That(indexedValue.name, Is.EqualTo(TEST_OBJ_NAME));
        }

        private void SetupForIndexing(int elementsCount)
        {
            var root = new VisualElement();
            elements = new ObjectField[elementsCount];
            for (int i = 0; i < elementsCount; i++)
            {
                elements[i] = CreateElementWithId(i);
                root.Add(elements[i]);
            }
            listField.SetupGet(mock => mock.ElementsRoot).Returns(root);
            sizeField.value = elementsCount;
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
        public void TestExceptionOnGetWithNegativeIndex()
        {
            SetupForIndexing(1);
            try
            {
                var obj = mapper[-1];
                Assert.Fail("Apparently mapper didn't throw any exception on negative index.");
            }
            catch (System.IndexOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestIndexingSet()
        {
            const string SET_OBJ_NAME = "Object to Set";
            SetupForIndexing(1);
            var objectToSet = ScriptableObject.CreateInstance<ScriptableObject>();
            objectToSet.name = SET_OBJ_NAME;

            mapper[0] = objectToSet;

            Assert.That(elements[0].value.name, Is.EqualTo(SET_OBJ_NAME));
        }

        [Test]
        public void TestIsReadOnlyFalse()
        {
            Assert.That(mapper.IsReadOnly, Is.False);
        }

        [Test]
        public void TestAddBehaviour()
        {
            SetupForIndexing(1);
            sizeField.value = 0;
            CreateTestObjectWithName(MISSING_OBJ_NAME);

            mapper.Add(testObject);

            Assert.That(sizeField.value, Is.EqualTo(1));
            Assert.That(elements[0].value.name, Is.EqualTo(MISSING_OBJ_NAME));
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
            SetupForIndexing(1);
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            Assert.That(mapper.Contains(testObject), Is.False);
        }

        [Test]
        public void TestContainsWhenShouldBeFound()
        {
            SetupForIndexing(1);
            Assert.That(mapper.Contains(testObject), Is.True);
        }

        [Test]
        public void TestCopyTo()
        {
            SetupForIndexing(1);
            var targetArray = new Object[1];
            mapper.CopyTo(targetArray, 0);
            Assert.That(targetArray[0].name, Is.EqualTo(TEST_OBJ_NAME));
        }

        [Test]
        public void TestGetEnumerator()
        {
            var enumerator = mapper.GetEnumerator();
            Assert.That(enumerator, Is.InstanceOf<List<Object>.Enumerator>());
        }

        [Test]
        public void TestIndexOfWhenShouldNotBeFound()
        {
            SetupForIndexing(1);
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            var foundId = mapper.IndexOf(testObject);
            Assert.That(foundId, Is.EqualTo(-1));
        }

        [Test]
        public void TestIndexOfWhenShouldBeFound()
        {
            SetupForIndexing(1);
            var foundId = mapper.IndexOf(testObject);
            Assert.That(foundId, Is.EqualTo(0));
        }

        [Test]
        public void TestExceptionOnInsertWithWrongIndex()
        {
            SetupForInsert();
            try
            {
                mapper.Insert(-1, testObject);
                Assert.Fail("Apparently insert didn't throw any exception on Insert with wrong index.");
            }
            catch (System.IndexOutOfRangeException)
            {
                Assert.That(sizeField.value, Is.EqualTo(1));
            }
        }

        private void SetupForInsert()
        {
            SetupForIndexing(2);
            sizeField.value = 1;
        }

        [Test]
        public void TestInsertIncrementsSizeField()
        {
            SetupForInsert();
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            mapper.Insert(0, testObject);
            Assert.That(sizeField.value, Is.EqualTo(2));
        }

        [Test]
        public void TestInsertShiftsObjectFieldValues()
        {
            SetupForInsert();
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            elements[0].value = testObject;
            mapper.Insert(0, testObject);
            Assert.That(elements[1].value.name, Is.EqualTo(MISSING_OBJ_NAME));
        }

        [Test]
        public void TestInsertSetsNewValue()
        {
            SetupForInsert();
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            mapper.Insert(0, testObject);
            Assert.That(elements[0].value.name, Is.EqualTo(MISSING_OBJ_NAME));
        }

        [Test]
        public void TestExceptionOnRemoveAtWithWrongIndex()
        {
            SetupForIndexing(2);
            try
            {
                mapper.RemoveAt(-1);
                Assert.Fail("Apparently RemoveAt didn't throw any exception on wrong index.");
            }
            catch (System.IndexOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestRemoveAtShiftsElementsBack()
        {
            SetupForIndexing(2);
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            elements[1].value = testObject;
            mapper.RemoveAt(0);
            Assert.That(elements[0].value.name, Is.EqualTo(MISSING_OBJ_NAME));
        }

        [Test]
        public void TestRemoveAtDecreasesSizeField()
        {
            SetupForIndexing(2);
            mapper.RemoveAt(0);
            Assert.That(sizeField.value, Is.EqualTo(1));
        }

        [Test]
        public void TestRemoveWhenShouldDoNothing()
        {
            SetupForIndexing(2);
            CreateTestObjectWithName(MISSING_OBJ_NAME);
            var removed = mapper.Remove(testObject);
            Assert.That(removed, Is.False);
            Assert.That(sizeField.value, Is.EqualTo(2));
        }

        [Test]
        public void TestRemove()
        {
            SetupForIndexing(2);
            var removed = mapper.Remove(testObject);
            Assert.That(removed);
            Assert.That(sizeField.value, Is.EqualTo(1));
        }
    }
}
