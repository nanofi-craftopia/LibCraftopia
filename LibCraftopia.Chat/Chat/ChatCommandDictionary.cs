using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Chat
{
    public class ChatCommandDictionary : ICollection<IChatCommand>
    {
        private readonly Dictionary<string, IChatCommand> dict = new Dictionary<string, IChatCommand>();
        public int Count => dict.Count;

        public bool IsReadOnly => false;

        public void Add(IChatCommand item)
        {
            dict.Add(item.Command, item);
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(IChatCommand item)
        {
            return dict.ContainsKey(item.Command);
        }

        public void CopyTo(IChatCommand[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IChatCommand> GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        public bool Remove(IChatCommand item)
        {
            return dict.Remove(item.Command);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        public IChatCommand GetOrDefault(string key)
        {
            return dict.GetOrDefault(key);
        }
    }
}
