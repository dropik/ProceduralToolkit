using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.UI
{
    public class ListField : BaseField<IList<Object>>, IListField
    {
        private Foldout foldout;
        private IntegerField sizeField;

        public IntegerField SizeField
        {
            get => sizeField;
            set
            {
                ElementsRoot.Add(value);
                sizeField = value;
            }
        }
        public VisualElement ElementsRoot => foldout;
        public IListElementFactory ElementFactory { get; set; }
        public IList<Object> ValueMapper { get; set; }
        public System.Type ObjectType { get; set; }

        public override IList<Object> value
        {
            get => ValueMapper;
            set
            {
                ValueMapper?.Clear();
                foreach (var item in value)
                {
                    ValueMapper.Add(item);
                }
            }
        }

        public ListField() : this("") { }
        public ListField(string label) : base(label, null)
        {
            CreateHierarchy();
        }

        private void CreateHierarchy()
        {
            InitFoldout();
            Add(foldout);
        }

        private void InitFoldout()
        {
            foldout = new Foldout()
            {
                value = false
            };
        }

        public void AddElement()
        {
            if (ElementFactory != null)
            {
                CreateNewElementWithId(LastIndex);
            }
        }

        private int LastIndex => value.Count - 1;

        private void CreateNewElementWithId(int id)
        {
            var newElement = ElementFactory.CreateElement(id);
            ElementsRoot.Add(newElement);
            if (id > 0)
            {
                value[id] = value[id - 1];
            }
        }

        public void RemoveElement()
        {
            var element = GetObjectFieldById(LastIndex);
            ElementsRoot.Remove(element);
        }

        private ObjectField GetObjectFieldById(int id)
        {
            return this.Query<ObjectField>($"element{id}").First();
        }
    }
}
