using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Rendering;

namespace LibCraftopia.Container
{
    public class PrioritiezedList<TPrior, T> : IList<T>
    {
        private List<TPrior> priors;
        private List<T> items;

        public PrioritiezedList()
        {
            priors = new List<TPrior>();
            items = new List<T>();
        }
        public PrioritiezedList(int capacity)
        {
            priors = new List<TPrior>(capacity);
            items = new List<T>(capacity);
        }

        public T this[int index] { get => items[index]; set => items[index] = value; }

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public TPrior GetPriority(int index)
        {
            return priors[index];
        }

        void ICollection<T>.Add(T item)
        {
            throw new InvalidOperationException();
        }
        public void Add(TPrior priority, T item)
        {
            var i = priors.BinarySearch(priority);
            if(i >= 0)
            {
                items.Insert(i, item);
                priors.Insert(i, priority);
            } else
            {
                items.Insert(~i, item);
                priors.Insert(~i, priority);
            }
        }

        public void Clear()
        {
            priors.Clear();
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new InvalidOperationException();
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if(index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
            priors.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
