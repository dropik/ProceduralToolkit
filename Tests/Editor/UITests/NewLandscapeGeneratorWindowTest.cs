using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class NewLandscapeGeneratorWindowTest
    {
        private NewLandscapeGeneratorWindow window;
        private ObjectField baseShapeField;

        private VisualElement Root =>
            window.rootVisualElement;

        private Button CreateButton =>
            Root.Query<Button>("createGenerator").First();

        private InspectorElement ParametersElement =>
            Root.Query<InspectorElement>().First();

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            window = EditorWindow.CreateWindow<NewLandscapeGeneratorWindow>();
            window.ParametersElement = CreateInspector();
            yield return null;
        }

        private InspectorElement CreateInspector()
        {
            var inspector = new InspectorElement();
            inspector.Add(BaseShapeField);
            return inspector;
        }

        private ObjectField BaseShapeField
        {
            get
            {
                if (baseShapeField == null)
                {
                    baseShapeField = new ObjectField()
                    {
                        objectType = typeof(BaseShapeGeneratorSettings)
                    };
                }
                return baseShapeField;
            }
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            window.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasCreateGeneratorButton()
        {
            Assert.That(CreateButton, Is.Not.Null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestInspectorElementAdded()
        {
            Assert.That(ParametersElement, Is.Not.Null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestCreateGeneratorIsNotActiveWhenBaseShapeIsNull()
        {
            Assert.That(CreateButton.enabledSelf, Is.False);
            yield return null;
        }
    }
}