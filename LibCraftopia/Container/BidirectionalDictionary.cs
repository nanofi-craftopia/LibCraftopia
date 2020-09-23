using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Container
{
    public interface IBidirectionalDictionary<T1, T2> : ICollection<KeyValuePair<T1, T2>>, IEnumerable<KeyValuePair<T1, T2>>
    {
        ICollection<T1> Lefts { get; }
        ICollection<T2> Rights { get; }

        T2 this[T1 key] { get; }

        void Add(T1 left, T2 right);
        bool ContainsLeft(T1 left);
        bool ContainsRight(T2 right);
        bool RemoveLeft(T1 left);
        bool RemoveRight(T2 right);
        bool TryGetLeft(T2 right, out T1 left);
        bool TryGetRight(T1 left, out T2 right);
        T1 GetLeft(T2 right);
        T2 GetRight(T1 left);
    }

    public class BidirectionalDictionary<T1, T2> : IBidirectionalDictionary<T1, T2>
    {
        private readonly Dictionary<T1, T2> leftDict;
        private readonly Dictionary<T2, T1> rightDict;

        public BidirectionalDictionary()
        {
            leftDict = new Dictionary<T1, T2>();
            rightDict = new Dictionary<T2, T1>();
        }

        public BidirectionalDictionary(IEnumerable<KeyValuePair<T1, T2>> enumerable) : this()
        {
            foreach (var item in enumerable)
            {
                Add(item);
            }
        }

        public T2 this[T1 key] { get => GetRight(key); }

        public ICollection<T1> Lefts => leftDict.Keys;

        public ICollection<T2> Rights => rightDict.Keys;

        public int Count => leftDict.Count;

        public bool IsReadOnly => false;

        public void Add(T1 left, T2 right)
        {
            leftDict.Add(left, right);
            rightDict.Add(right, left);
        }

        public void Add(KeyValuePair<T1, T2> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            leftDict.Clear();
            rightDict.Clear();
        }

        public bool Contains(KeyValuePair<T1, T2> item)
        {
            return ContainsLeft(item.Key) && ContainsRight(item.Value);
        }

        public bool ContainsLeft(T1 left)
        {
            return leftDict.ContainsKey(left);
        }

        public bool ContainsRight(T2 right)
        {
            return rightDict.ContainsKey(right);
        }

        public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return leftDict.GetEnumerator();
        }

        public T1 GetLeft(T2 right)
        {
            return rightDict[right];
        }

        public T2 GetRight(T1 left)
        {
            return leftDict[left];
        }

        public bool Remove(KeyValuePair<T1, T2> item)
        {
            return leftDict.Remove(item.Key) && rightDict.Remove(item.Value);
        }

        public bool RemoveLeft(T1 left)
        {
            T2 right;
            if (TryGetRight(left, out right))
            {
                return leftDict.Remove(left) && rightDict.Remove(right);
            }
            return false;
        }

        public bool RemoveRight(T2 right)
        {
            T1 left;
            if (TryGetLeft(right, out left))
            {
                return leftDict.Remove(left) && rightDict.Remove(right);
            }
            return false;
        }

        public bool TryGetLeft(T2 right, out T1 left)
        {
            return rightDict.TryGetValue(right, out left);
        }

        public bool TryGetRight(T1 left, out T2 right)
        {
            return leftDict.TryGetValue(left, out right);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return leftDict.GetEnumerator();
        }
    }
}
