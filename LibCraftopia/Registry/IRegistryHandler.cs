using Cysharp.Threading.Tasks;
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
        int UserMinId { get; }

        void OnRegister(string key, int id, T value);
        void OnUnregister(string key, int id);
        UniTask Init(Registry<T> registry);
        UniTask Apply(ICollection<T> elements);
    }
}
