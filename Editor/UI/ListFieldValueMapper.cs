using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public class ListFieldValueMapper : IList<Object>
    {
        private readonly IListField listField;

        public ListFieldValueMapper(IListField listField)
        {
            this.listField = listField;
        }

        public Object this[int index]
        {
            get
            {
                var element = GetElementById(index);
                return element.value;
            }
            set
            {
                var element = GetElementById(index);
                element.value = value;
            }
        }

        private ObjectField GetElementById(int id)
        {
            var root = listField.ElementsRoot;
            var foundElement = root.Query<ObjectField>($"element{id}").First();
            if (foundElement is null)
            {
                throw new System.IndexOutOfRangeException("Out of bounds when trying to access ListField element.");
            }
            return foundElement;
        }

        public int Count
        {
            get => listField.SizeField.value;
            private set
            {
                listField.SizeField.value = value;
            }
        }

        public bool IsReadOnly => false;

        public void Add(Object item)
        {
            Count++;
            this[Count - 1] = item;
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(Object item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Object[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<Object> GetEnumerator()
        {
            return new List<Object>.Enumerator();
        }

        public int IndexOf(Object item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, Object item)
        {
            CheckOutOfBounds(index);
            Count++;
            ShiftValuesForwardFrom(index);
            this[index] = item;
        }

        private void ShiftValuesForwardFrom(int index)
        {
            for (int i = Count - 1; i > index; i--)
            {
                this[i] = this[i - 1];
            }
        }

        private void CheckOutOfBounds(int index)
        {
            _ = this[index];
        }

        public bool Remove(Object item)
        {
            var foundId = IndexOf(item);
            if (foundId < 0)
            {
                return false;
            }
            RemoveAt(foundId);
            return true;
        }

        public void RemoveAt(int index)
        {
            CheckOutOfBounds(index);
            ShiftValuesBackFrom(index);
            Count--;
        }

        private void ShiftValuesBackFrom(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                this[i] = this[i + 1];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
