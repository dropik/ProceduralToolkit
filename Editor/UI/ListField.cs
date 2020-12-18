using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.UI
{
    public class ListField : BaseField<List<Object>>, IListField
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
        public IListElementFactory ElementFactory { get; set; }
        public System.Type ObjectType { get; set; }

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
            value.Add(null);
            var id = value.Count - 1;
            if (ElementFactory != null)
            {
                CreateNewElementWithId(id);
            }
        }

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
            var prevElement = this.Query<ObjectField>($"element{id - 1}").First();
            newElement.value = prevElement.value;
        }

        public void RemoveElement()
        {
            var id = value.Count - 1;
            var element = this.Query<ObjectField>($"element{id}").First();
            foldout.Remove(element);
            value.RemoveAt(id);
        }

        public void UpdateValueAt(int id)
        {
            var updatedElement = this.Query<ObjectField>($"element{id}").First();
            value[id] = updatedElement.value;
        }
    }
}
