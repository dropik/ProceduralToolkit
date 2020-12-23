using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    [CustomEditor(typeof(GeneratorBootDescriptor))]
    public class GeneratorBootDescriptorEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var uxml = Resources.Load<VisualTreeAsset>("Layouts/generator-boot-descriptor-editor");
            return uxml.CloneTree();
        }
    }
}
