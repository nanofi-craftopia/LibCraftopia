using Cysharp.Threading.Tasks;
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
using UnityEngine;

namespace LibCraftopia.Registry
{
    public class Registry<T> : IRegistry where T : IRegistryEntry
    {
        private readonly IRegistryHandler<T> handler;
        private BidirectionalDictionary<string, int> keyIdDict = new BidirectionalDictionary<string, int>();
        private Dictionary<string, T> elements = new Dictionary<string, T>();
        private int currentId;

        public string Name { get; private set; }
        public int MinId { get => handler.MinId; }
        public int MaxId { get => handler.MaxId; }
        public int UserMinId { get => handler.UserMinId; }

        public ICollection<T> Elements { get => elements.Values; }
        public IReadOnlyDictionary<string, T> KeyedElements { get => elements; }

        internal Registry(string name, IRegistryHandler<T> handler)
        {
            Name = name;
            this.handler = handler;
            currentId = UserMinId;
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

        public bool TryGetElement(string key, out T value)
        {
            return elements.TryGetValue(key, out value);
        }

        public T GetElementById(int id)
        {
            if (keyIdDict.TryGetLeft(id, out var key))
            {
                if (elements.TryGetValue(key, out var element))
                {
                    return element;
                }
            }
            return default(T);
        }
        internal UniTask RegisterVanillaElements(IEnumerable<T> elements, Func<T, string> keyGen, Func<T, object> conflictInfo = null)
        {
            return UniTask.RunOnThreadPool(() =>
            {
                var counts = new Dictionary<string, int>();
                var list = new List<Tuple<string, T>>();
                foreach (var elem in elements)
                {
                    string key = keyGen(elem);
                    counts.Increment(key);
                    list.Add(Tuple.Create(key, elem));
                }
                var unique = new Dictionary<string, int>();
                foreach (var tuple in list)
                {
                    var key = tuple.Item1;
                    var elem = tuple.Item2;
                    if (counts[key] > 1)
                    {
                        var info = conflictInfo?.Invoke(elem);
                        Logger.Inst.LogWarning($"Confliction: {elem.Id}, {key}, {info}");
                        unique.Increment(key);
                        key += $"-{unique[key]}";
                    }
                    this.RegisterVanilla(key, elem);
                }
            }).LogError();
        }

        internal void RegisterVanilla(string key, T value)
        {
            if (elements.ContainsKey(key))
            {
                throw new ArgumentException($"The key, {key}, already exists.", "key");
            }
            if (keyIdDict.TryGetRight(key, out var savedId))
            {
                if (value.Id != savedId)
                {
                    Logger.Inst.LogWarning($"Register({Name}): try to register key={key} with id={value.Id}, but the key already exists with the different id={savedId}. Maybe the original game have been updated.");
                    keyIdDict.RemoveLeft(key);
                }
            }
            if (keyIdDict.TryGetLeft(value.Id, out var savedKey))
            {
                if (key != savedKey)
                {
                    Logger.Inst.LogWarning($"Register({Name}): try to register id={value.Id} with key={key}, but the id already exists with different key {savedKey}");
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
            while (keyIdDict.ContainsRight(currentId))
            {
                currentId++;
                if (currentId >= MaxId)
                {
                    throw new InvalidOperationException($"Register({Name}): id runs out. New id {currentId} v.s. Max id {MaxId}.");
                }
            }
            return currentId;
        }

        async UniTask IRegistry.Init()
        {
            Logger.Inst.LogInfo($"Register({Name}): initialize start.");
            await handler.Init(this);
            Logger.Inst.LogInfo($"Register({Name}): initialize end.");
        }

        async UniTask IRegistry.Apply()
        {
            Logger.Inst.LogInfo($"Register({Name}): application starts.");
            await handler.Apply(elements.Values);
            await detectUnknownKeys();
            Logger.Inst.LogInfo($"Register({Name}): application ends.");
        }

        private UniTask detectUnknownKeys()
        {
            return UniTask.RunOnThreadPool(() =>
            {
                var unknownKeys = new List<string>();
                foreach (var key in keyIdDict.Lefts)
                {
                    if (!elements.ContainsKey(key)) unknownKeys.Add(key);
                }
                foreach (var key in unknownKeys)
                {
                    keyIdDict.RemoveLeft(key);
                    Logger.Inst.LogWarning($"The key, {key}, was stored in the registry but has not been registered by both the main game and mods. Deleted from the registry.");
                }
            }).LogError();
        }

        private string Filename => $"{Name}.regist";


        private const int VERSION = 1;

        UniTask IRegistry.Load(string baseDir)
        {
            return UniTask.RunOnThreadPool(() =>
            {
                string path = Path.Combine(baseDir, Filename);
                if (!File.Exists(path)) return;
                keyIdDict.Clear();
                elements.Clear();
                using (var reader = new BinaryReader(File.OpenRead(path)))
                {
                    try
                    {
                        var version = reader.ReadInt32();
                        switch (version)
                        {
                            case 1:
                            default:
                                loadVersion1(reader);
                                break;
                        }
                    }
                    catch
                    {
                        keyIdDict.Clear();
                        elements.Clear();
                        try
                        {
                            File.Delete(path);
                        }
                        catch { }
                    }
                }
            }).LogError();
        }

        private void loadVersion1(BinaryReader reader)
        {
            int size = reader.ReadInt32();
            for (int i = 0; i < size; i++)
            {
                string key = reader.ReadString();
                int id = reader.ReadInt32();
                keyIdDict.Add(key, id);
            }
        }

        UniTask IRegistry.Save(string baseDir)
        {
            return UniTask.RunOnThreadPool(() =>
            {
                string path = Path.Combine(baseDir, Filename);
                using (var writer = new BinaryWriter(File.OpenWrite(path)))
                {
                    writer.Write(VERSION);
                    writer.Write(keyIdDict.Count);
                    foreach (var item in keyIdDict)
                    {
                        writer.Write(item.Key);
                        writer.Write(item.Value);
                    }
                }
            });
        }
    }
}
