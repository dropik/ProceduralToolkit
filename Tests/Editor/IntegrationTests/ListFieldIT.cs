using NUnit.Framework;
using ProceduralToolkit.EditorTests.UITests;
using ProceduralToolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.IntegrationTests
{
    public class ListFieldIT : BaseUxmlCustomVETest
    {
        private ListContainer listContainer;

        private const string TEST_OBJ_NAME = "Test Object";

        internal class ListContainer : ScriptableObject
        {
            public List<Object> list = new List<Object>();
        }

        protected override string TestLayout => "list-field-test";

        protected override void PreWindowCreation()
        {
            listContainer = ScriptableObject.CreateInstance<ListContainer>();
        }

        protected override VisualElement CreateTestTarget()
        {
            var target = base.CreateTestTarget();
            var listField = target.Query<ListField>().First();
            listField.bindingPath = "list";
            return target;
        }

        protected override void PostWindowCreation()
        {
            var serializedObject = new SerializedObject(listContainer);
            RootVisualElement.Bind(serializedObject);
        }

        protected override void PreWindowClose()
        {
            Object.DestroyImmediate(listContainer);
        }

        [UnityTest]
        public IEnumerator TestEntireValueModifiedOnListField()
        {
            var newList = new List<Object>();
            var newObject = ScriptableObject.CreateInstance<ScriptableObject>();
            newObject.name = TEST_OBJ_NAME;

            TargetListField.value = newList;

            yield return SkipFrames();

            Assert.That(listContainer.list.Count, Is.EqualTo(1));
            Assert.That(listContainer.list[0].name, Is.EqualTo(TEST_OBJ_NAME));
        }

        private ListField TargetListField => RootVisualElement.Query<ListField>().First();
    }
}
