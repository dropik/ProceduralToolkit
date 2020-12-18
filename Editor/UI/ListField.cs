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
                foldout.Add(value);
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
            InitList();
            CreateHierarchy();
        }

        private void InitList()
        {
            if (value == null)
            {
                value = new List<Object>();
            }
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
            var id = LastIndex;
            if (ElementFactory != null)
            {
                CreateNewElementWithId(id);
            }
        }

        private int LastIndex => SizeField.value - 1;

        private void CreateNewElementWithId(int id)
        {
            var newElement = ElementFactory.CreateElement(id);
            foldout.Add(newElement);
            if (id > 0)
            {
                CopyPreviousElement(id, newElement);
            }
        }

        private void CopyPreviousElement(int id, ObjectField newElement)
        {
            var prevElement = GetObjectFieldById(id - 1);
            newElement.value = prevElement.value;
        }

        private ObjectField GetObjectFieldById(int id)
        {
            return this.Query<ObjectField>($"element{id}").First();
        }

        public void RemoveElement()
        {
            var id = LastIndex;
            var element = GetObjectFieldById(id);
            foldout.Remove(element);
        }
    }
}
