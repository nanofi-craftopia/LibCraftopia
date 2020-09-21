using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Registry
{
    public interface IRegistryHandler<T> where T : IRegistryEntry
    {
        int MaxId { get; }
        int MinId { get; }
        bool IsGameDependent { get; }

        void OnRegister(string key, int id, T value);
        void OnUnregister(string key, int id);
        IEnumerator Apply(ICollection<T> elements);
    }
}
