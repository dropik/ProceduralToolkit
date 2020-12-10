using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit
{
    public abstract class EditorBase<TEditor> : Editor
        where TEditor : EditorBase<TEditor>
    {
        private VisualElement rootVisualElement;

        protected VisualElement RootVisualElement
        {
            get
            {
                if (rootVisualElement == null)
                {
                    rootVisualElement = new VisualElement();
                }
                return rootVisualElement;
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            ResetRootElement();
            LoadLayoutResource();
            AddCallbacksToVisualElements();
            AddCommonEditorStyleSheet();
            BindToRoot();
            return RootVisualElement;
        }

        private void ResetRootElement()
        {
            rootVisualElement = new VisualElement();
        }

        private void LoadLayoutResource()
        {
            var visualTree = Resources.Load<VisualTreeAsset>(LayoutPath);
            if (visualTree != null)
            {
                visualTree.CloneTree(RootVisualElement);
            }
        }

        protected virtual string LayoutPath => LayoutPathAttribute?.Path;

        private LayoutPathAttribute LayoutPathAttribute
        {
            get
            {
                return System.Attribute.GetCustomAttribute(
                    typeof(TEditor), typeof(LayoutPathAttribute)) as LayoutPathAttribute;
            }
        }

        protected virtual void AddCallbacksToVisualElements() { }

        private void AddCommonEditorStyleSheet()
        {
            var styleSheet = Resources.Load<StyleSheet>("Styles/common-editor");
            RootVisualElement.styleSheets.Add(styleSheet);
        }

        private void BindToRoot()
        {
            BindTargetToRoot();
            BindThisToRoot();
        }

        private void BindTargetToRoot()
        {
            BindObjectToRoot(target);
        }

        private void BindObjectToRoot(Object obj)
        {
            var serializedObject = new SerializedObject(obj);
            RootVisualElement.Bind(serializedObject);
        }

        private void BindThisToRoot()
        {
            BindObjectToRoot(this);
        }
    }
}
