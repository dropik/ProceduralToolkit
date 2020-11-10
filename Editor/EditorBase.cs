using System;
using UnityEditor;
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
            RootVisualElement.Clear();
            LoadLayoutResource();
            return RootVisualElement;
        }

        private void LoadLayoutResource()
        {
            var visualTree = Resources.Load(LayoutPath) as VisualTreeAsset;
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
                return Attribute.GetCustomAttribute(
                    typeof(TEditor), typeof(LayoutPathAttribute)) as LayoutPathAttribute;
            }
        }
    }
}
