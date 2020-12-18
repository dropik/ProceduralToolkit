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
            return root.Query<ObjectField>($"element{id}").First();
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
            throw new System.NotImplementedException();
        }

        public IEnumerator<Object> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(Object item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, Object item)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Object item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
