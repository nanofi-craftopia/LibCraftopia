using BepInEx;
using LibCraftopia.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Registry
{
    public class RegistryManager : SingletonMonoBehaviour<RegistryManager>
    {
        private readonly string baseDir = Path.Combine(Paths.ConfigPath, "LibCraftopia");
        private readonly Dictionary<Type, IRegistry> registries = new Dictionary<Type, IRegistry>();

        public bool Initialized { get; private set; } = false;

        protected override void OnUnityAwake()
        {
            LoadingManager.Inst.InitializeLoaders.Add(20, initialize);
            LoadingManager.Inst.InitializeLoaders.Add(99999, apply);
            LoadingManager.Inst.InitializeGameLoaders.Add(99999, applyGame);
        }

        private IEnumerator initialize(bool needStabilization)
        {
            foreach (var registry in registries.Values)
            {
                var task = registry.Load(baseDir);
                while (!task.IsCompleted && !task.IsCanceled)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                yield return registry.Init();
            }
            Initialized = true;
            yield break;
        }

        private IEnumerator apply(bool needStabilization)
        {
            foreach (var registry in registries.Values)
            {
                if (!registry.IsGameDependent)
                {
                    yield return registry.Apply();
                    Directory.CreateDirectory(baseDir);
                    var task = registry.Save(baseDir);
                    while (!task.IsCompleted && !task.IsCanceled)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }

        private IEnumerator applyGame(bool needStabilization)
        {
            foreach (var registry in registries.Values)
            {
                if (registry.IsGameDependent)
                {
                    var enumerator = registry.Apply();
                    while (enumerator.MoveNext()) yield return enumerator.Current;
                    var task = registry.Save(baseDir);
                    while (!task.IsCompleted && !task.IsCanceled)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }
        }


        public Registry<T> CreateRegistry<T>(IRegistryHandler<T> handler) where T : IRegistryEntry
        {
            if (Initialized)
            {
                throw new Exception("A registry must be created before initialization. Add a registry in a coroutine added to `LoadingManager.Inst.InitializeLoaders` with a priority less than 20.");
            }
            var key = typeof(T);
            if (registries.ContainsKey(key))
            {
                throw new ArgumentException(string.Format("A registry for {0} is already created.", key.Name));
            }
            var name = key.Name;
            var registry = new Registry<T>(name, handler);
            registries.Add(key, registry);
            return registry;
        }

        public Registry<T> GetRegistry<T>() where T : IRegistryEntry
        {
            if (!Initialized)
            {
                throw new Exception("We cannot access to registries before initialization. This method can be accessed from coroutines added to `LoadingManager.Inst.InitializeLoaders` with a priority grater than 20 or coroutines added to `LoadingManager.Inst.InitializeGameLoaders`.");
            }
            var key = typeof(T);
            if (registries.TryGetValue(key, out var registry))
            {
                return (Registry<T>)registry;
            }
            return null;
        }
    }
}
