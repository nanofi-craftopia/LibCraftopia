using LibCraftopia.Container;
using LibCraftopia.Utils;
using Oc.Em;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LibCraftopia.Registry
{
    public interface IRegistry
    {
        bool IsGameDependent { get; }

        IEnumerator Init();
        IEnumerator Apply();

        Task Load(string baseDir);
        Task Save(string baseDir);
    }

    public class Registry<T> : IRegistry where T : IRegistryEntry
    {
        private readonly IRegistryHandler<T> handler;
        private BidirectionalDictionary<string, int> keyIdDict = new BidirectionalDictionary<string, int>();
        private Dictionary<string, T> elements = new Dictionary<string, T>();
        private int currentId;

        public string Name { get; private set; }
        public int MinId { get => handler.MinId; }
        public int MaxId { get => handler.MaxId; }
        public bool IsGameDependent { get => handler.IsGameDependent; }

        public ICollection<T> Elements { get => elements.Values; }

        internal Registry(string name, IRegistryHandler<T> handler)
        {
            Name = name;
            this.handler = handler;
            currentId = handler.MinId;
        }

        public void Register(string key, T value)
        {
            if (elements.ContainsKey(key))
            {
                throw new ArgumentException("The key already exists", "key");
            }
            int id;
            if (!keyIdDict.TryGetRight(key, out id))
            {
                id = findNewId();
                keyIdDict.Add(key, id);
            }
            value.Id = id;
            elements.Add(key, value);
            Logger.Inst.LogInfo($"Register({Name}): register a new element. \"{key}\" \"{id}\"");
            handler.OnRegister(key, id, value);
        }

        public bool ExistsKey(string key)
        {
            return elements.ContainsKey(key);
        }

        public T GetElement(string key)
        {
            return elements[key];
        }

        internal void RegisterVanilla(string key, T value)
        {
            if (elements.ContainsKey(key))
            {
                throw new ArgumentException($"The key, {key}, already exists.", "key");
            }
            if (keyIdDict.TryGetLeft(value.Id, out var savedKey))
            {
                if (key != savedKey)
                {
                    Logger.Inst.LogError($"Register({Name}): try to register id={value.Id} with key={key}, but the id already exists with different key {savedKey}");
                    keyIdDict.RemoveRight(value.Id);
                    keyIdDict.Add(key, value.Id);
                }
            }
            else
            {
                keyIdDict.Add(key, value.Id);
            }
            elements.Add(key, value);
            currentId = Math.Max(currentId, value.Id + 1);
        }

        public bool Unregister(string key)
        {
            int id;
            if (keyIdDict.TryGetRight(key, out id))
            {
                var result = keyIdDict.RemoveLeft(key) && elements.Remove(key);
                handler.OnUnregister(key, id);
                return result;
            }
            return false;
        }

        private int findNewId()
        {
            var newId = currentId++;
            if (newId >= MaxId)
            {
                throw new InvalidOperationException($"Register({Name}): id runs out. New id {newId} v.s. Max id {MaxId}.");
            }
            return newId;
        }

        public IEnumerator Init()
        {
            Logger.Inst.LogInfo($"Register({Name}): initialize start.");
            var enumerator = handler.Init(this);
            while (enumerator.MoveNext()) yield return enumerator.Current;
            Logger.Inst.LogInfo($"Register({Name}): initialize end.");
        }

        public IEnumerator Apply()
        {
            Logger.Inst.LogInfo($"Register({Name}): applying to the game starts.");
            var enumerator = handler.Apply(elements.Values);
            while (enumerator.MoveNext()) yield return enumerator.Current;
            Logger.Inst.LogInfo($"Register({Name}): applying to the game ends.");
        }

        private string Filename
        {
            get => string.Format("{0}.regist", Name);
        }

        public Task Load(string baseDir)
        {
            return Task.Run(() =>
            {
                string path = Path.Combine(baseDir, Filename);
                if (!File.Exists(path)) return;
                keyIdDict.Clear();
                elements.Clear();
                using (var reader = new BinaryReader(File.OpenRead(path)))
                {
                    currentId = reader.ReadInt32();
                    int size = reader.ReadInt32();
                    for (int i = 0; i < size; i++)
                    {
                        string key = reader.ReadString();
                        int id = reader.ReadInt32();
                        keyIdDict.Add(key, id);
                    }
                }
                if (currentId < MinId)
                {
                    Logger.Inst.LogWarning($"Register({Name}): The saved current id ({currentId}) conflicts the min id ({MinId}). Set the min id to current id.");
                    currentId = MinId;
                }
            }).LogError();
        }

        public Task Save(string baseDir)
        {
            return Task.Run(() =>
            {
                string path = Path.Combine(baseDir, Filename);
                using (var writer = new BinaryWriter(File.OpenWrite(path)))
                {
                    writer.Write(currentId);
                    writer.Write(keyIdDict.Count);
                    foreach (var item in keyIdDict)
                    {
                        writer.Write(item.Key);
                        writer.Write(item.Value);
                    }
                }
            }).LogError();
        }
    }
}
