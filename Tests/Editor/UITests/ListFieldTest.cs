using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldTest
    {
        private ListField listField;
        private EditorWindow window;
        private IntegerField sizeField;

        private const string TEST_OBJ_NAME = "Some Object";

        [SetUp]
        public void SetUp()
        {
            InitFieldParts();
            InitWindow();
        }

        private void InitFieldParts()
        {
            listField = new ListField()
            {
                ObjectType = typeof(ScriptableObject)
            };
            sizeField = listField.Query<IntegerField>("size").First();
        }

        private void InitWindow()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            window.rootVisualElement.Add(listField);
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
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
        public void TestIncrementingSizeByOneAddsOnlyOneElement()
        {
            sizeField.value++;

            var elements = listField.Query<ObjectField>().ToList();
            Assert.That(elements.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestFirstNewElement()
        {
            sizeField.value++;

            var element0 = listField.Query<ObjectField>("element0").First();
            Assert.That(element0, Is.Not.Null);
            Assert.That(element0.label, Is.EqualTo("Element 0"));
            Assert.That(element0.value, Is.Null);
            Assert.That(SelectorTextOf(element0), Is.EqualTo("None (Scriptable Object)"));
        }

        private string SelectorTextOf(ObjectField element)
        {
            var elementSelectorLabel = element.Query<Label>().Last();
            return elementSelectorLabel.text;
        }

        [Test]
        public void TestNewElementWhenPreviousWasNull()
        {
            sizeField.value = 2;

            var element1 = listField.Query<ObjectField>("element1").First();
            Assert.That(SelectorTextOf(element1), Is.EqualTo("None (Scriptable Object)"));
        }

        [Test]
        public void TestNewElementWhenPreviousWasNotNull()
        {
            SetupFirstElement();

            sizeField.value++;

            var element1 = listField.Query<ObjectField>("element1").First();
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
            sizeField.value = INITIAL_SIZE;

            sizeField.value = -1;

            Assert.That(sizeField.value, Is.EqualTo(INITIAL_SIZE));
            var elements = listField.Query<ObjectField>().ToList();
            Assert.That(elements.Count, Is.EqualTo(INITIAL_SIZE));
        }

        [Test]
        public void TestValuePropertyUpdatedOnAdd()
        {
            SetupFirstElement();
            AssertThatListIsCorrect(expectedLength: 1, checkId: 0);

            sizeField.value++;
            AssertThatListIsCorrect(expectedLength: 2, checkId: 1);
        }

        private void AssertThatListIsCorrect(int expectedLength, int checkId)
        {
            var value = listField.value;
            Assert.That(value.Count, Is.EqualTo(expectedLength));
            Assert.That(value[checkId].name, Is.EqualTo(TEST_OBJ_NAME));
        }

        [Test]
        public void TestValuePropertyUpdatedOnRemove()
        {
            SetupFirstElement();
            sizeField.value = 3;
            sizeField.value = 2;

            AssertThatListIsCorrect(expectedLength: 2, checkId: 1);
        }
    }
}
