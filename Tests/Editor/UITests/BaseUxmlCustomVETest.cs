using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public abstract class BaseUxmlCustomVETest : BaseCustomVETest
    {
        protected override VisualElement CreateTestTarget()
        {
            var root = new VisualElement();
            var uxml = Resources.Load<VisualTreeAsset>(TestLayout);
            uxml.CloneTree(root);
            return root;
        }

        protected abstract string TestLayout { get; }
    }
}
