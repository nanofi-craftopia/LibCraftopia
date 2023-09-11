using BepInEx;
using Cysharp.Threading.Tasks;
using LibCraftopia.Initialize;
using LibCraftopia.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Registry
{
    public class RegistryManager : SingletonMonoBehaviour<RegistryManager>
    {
        public class RegistryInitHandler : IInitializeHandler
        {
            private readonly RegistryManager parent;

            public RegistryInitHandler(RegistryManager parent)
            {
                this.parent = parent;
            }

            public async UniTask Init(InitializeContext context)
            {
                context.Message = "Initialize registries";
                var index = 0;
                foreach (var registry in parent.registries.Values)
                {
                    context.Message = $"Initialize registry ({registry.Name})";
                    await registry.Load(parent.baseDir);
                    await registry.Init();
                    context.Progress = index / (float)parent.registries.Count;
                    index += 1;
                }
                parent.Initialized = true;
            }
        }
        public class RegistryApplyHandler : IInitializeHandler
        {
            private readonly RegistryManager parent;

            public RegistryApplyHandler(RegistryManager parent)
            {
                this.parent = parent;
            }

            public async UniTask Init(InitializeContext context)
            {
                context.Message = "Apply registries";
                var index = 0;
                foreach (var registry in parent.registries.Values)
                {
                    context.Message = $"Apply registry ({registry.Name})";
                    await registry.Apply();
                    Directory.CreateDirectory(parent.baseDir);
                    await registry.Save(parent.baseDir);
                    context.Progress = index / (float)parent.registries.Count;
                    index += 1;
                }
            }
        }


        private readonly string baseDir = Path.Combine(Paths.ConfigPath, "LibCraftopia");
        private readonly Dictionary<Type, IRegistry> registries = new Dictionary<Type, IRegistry>();
        public bool Initialized { get; private set; } = false;

        protected override void OnUnityAwake()
        {
            InitializeManager.Inst.AddHandler(InitializeManager.RegistryInit, new RegistryInitHandler(this));
            InitializeManager.Inst.AddHandler(InitializeManager.RegistryApply, new RegistryApplyHandler(this));
        }

        public Registry<T> CreateRegistry<T>(IRegistryHandler<T> handler) where T : IRegistryEntry
        {
            if (Initialized)
            {
                throw new Exception("A registry must be created before initialization.");
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
                throw new Exception("A registry cannot be obtained before initialization.");
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
