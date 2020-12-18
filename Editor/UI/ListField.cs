using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.UI
{
    public class ListField : BaseField<List<Object>>
    {
        private Foldout foldout;
        private IntegerField sizeField;

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
            InitSizeField();
            foldout.Add(sizeField);
            Add(foldout);
        }

        private void InitFoldout()
        {
            foldout = new Foldout()
            {
                value = false
            };
        }

        private void InitSizeField()
        {
            sizeField = new IntegerField()
            {
                name = "size",
                label = "Size",
                value = 0
            };
            sizeField.RegisterValueChangedCallback(OnSizeChanged);
        }

        private void OnSizeChanged(ChangeEvent<int> e)
        {
            if (NewValueIsNegative(e))
            {
                RestorePreviousValue(e);
                return;
            }
            HandlePositiveValue(e);
        }

        private bool NewValueIsNegative(ChangeEvent<int> e)
        {
            return e.newValue < 0;
        }

        private void RestorePreviousValue(ChangeEvent<int> e)
        {
            sizeField.SetValueWithoutNotify(e.previousValue);
        }

        private void HandlePositiveValue(ChangeEvent<int> e)
        {
            if (IncrementedValue(e))
            {
                AddElements(e);
            }
            else if (DecrementedValue(e))
            {
                RemoveElements(e);
            }
        }

        private bool IncrementedValue(ChangeEvent<int> e)
        {
            return e.newValue > e.previousValue;
        }

        private void AddElements(ChangeEvent<int> e)
        {
            value.Capacity = e.newValue;
            for (int i = e.previousValue; i < e.newValue; i++)
            {
                AddNewElementWithId(i);
            }
        }

        private void AddNewElementWithId(int id)
        {
            // Just allocate space for an eventual object.
            // Actual object will be passed via ObjectField value change callback.
            value.Add(null);

            var newElement = InitNewElement(id);
            foldout.Add(newElement);

            if (id > 0)
            {
                CopyPreviousElement(id, newElement);
            }
        }

        private ObjectField InitNewElement(int id)
        {
            return ElementFactory.CreateElement(id);
        }

        private void CopyPreviousElement(int id, ObjectField newElement)
        {
            var prevElement = this.Query<ObjectField>($"element{id - 1}").First();
            newElement.value = prevElement.value;
        }

        private bool DecrementedValue(ChangeEvent<int> e)
        {
            return e.newValue < e.previousValue;
        }

        private void RemoveElements(ChangeEvent<int> e)
        {
            for (int i = e.previousValue - 1; i >= e.newValue; i--)
            {
                RemoveElementAtId(i);
            }
        }

        private void RemoveElementAtId(int id)
        {
            var element = this.Query<ObjectField>($"element{id}").First();
            foldout.Remove(element);
            value.RemoveAt(id);
        }
    }
}
