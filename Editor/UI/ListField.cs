using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.UI
{
    public class ListField : BaseField<List<Object>>
    {
        private Foldout foldout;

        public System.Type ObjectType { get; set; }

        public ListField() : this("") { }

        public ListField(string label) : base(label, null)
        {
            foldout = new Foldout()
            {
                value = false
            };

            var sizeField = new IntegerField()
            {
                name = "size",
                label = "Size",
                value = 0
            };
            sizeField.RegisterValueChangedCallback(OnSizeChanged);

            foldout.Add(sizeField);
            Add(foldout);
        }

        private void OnSizeChanged(ChangeEvent<int> e)
        {
            if (e.newValue > e.previousValue)
            {
                AddElements(e);
            }
            else if (e.newValue < e.previousValue)
            {
                RemoveElements(e);
            }
        }

        private void AddElements(ChangeEvent<int> e)
        {
            for (int i = e.previousValue; i < e.newValue; i++)
            {
                AddingNewElement(i);
            }
        }

        private void AddingNewElement(int id)
        {
            var newElement = InitNewElement(id);
            if (id > 0)
            {
                CopyPreviousElement(id, newElement);
            }
            foldout.Add(newElement);
        }

        private ObjectField InitNewElement(int id)
        {
            return new ObjectField()
            {
                name = $"element{id}",
                label = $"Element {id}",
                objectType = ObjectType
            };
        }

        private void CopyPreviousElement(int id, ObjectField newElement)
        {
            var prevElement = this.Query<ObjectField>($"element{id - 1}").First();
            newElement.value = prevElement.value;
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
        }
    }
}
