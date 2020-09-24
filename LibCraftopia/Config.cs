using BepInEx;
using BepInEx.Configuration;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia
{
    internal class Config : SingletonMonoBehaviour<Config>, IDictionary<ConfigDefinition, ConfigEntryBase>
    {
        private ConfigFile config;

        protected override void OnUnityAwake()
        {
        }

        public void Init(ConfigFile config)
        {
            this.config = config;
        }


        public string ConfigFilePath => config.ConfigFilePath;
        public bool SaveOnConfigSet { get => config.SaveOnConfigSet; set => config.SaveOnConfigSet = value; }

        public ICollection<ConfigDefinition> Keys => config.Keys;

        ICollection<ConfigEntryBase> IDictionary<ConfigDefinition, ConfigEntryBase>.Values => ((IDictionary<ConfigDefinition, ConfigEntryBase>)config).Values;

        public bool IsReadOnly => config.IsReadOnly;

        public int Count => config.Count;

        ConfigEntryBase IDictionary<ConfigDefinition, ConfigEntryBase>.this[ConfigDefinition key] { get => config[key]; set => throw new InvalidOperationException(); }
        public ConfigEntryBase this[ConfigDefinition key] => config[key];
        public ConfigEntryBase this[string section, string key] => config[section, key];


        public void Reload() => config.Reload();
        public void Save() => config.Save();
        public bool TryGetEntry<T>(ConfigDefinition configDefinition, out ConfigEntry<T> entry) => config.TryGetEntry(configDefinition, out entry);
        public bool TryGetEntry<T>(string section, string key, out ConfigEntry<T> entry) => config.TryGetEntry(section, key, out entry);
        public ConfigEntry<T> Bind<T>(ConfigDefinition configDefinition, T defaultValue, ConfigDescription configDescription = null) => config.Bind(configDefinition, defaultValue, configDescription);
        public ConfigEntry<T> Bind<T>(string section, string key, T defaultValue, ConfigDescription configDescription = null) => config.Bind(section, key, defaultValue, configDescription);
        public ConfigEntry<T> Bind<T>(string section, string key, T defaultValue, string description) => config.Bind(section, key, defaultValue, description);

        public void Add(ConfigDefinition key, ConfigEntryBase value) => config.Add(key, value);

        public bool Remove(ConfigDefinition key) => config.Remove(key);

        void ICollection<KeyValuePair<ConfigDefinition, ConfigEntryBase>>.Add(KeyValuePair<ConfigDefinition, ConfigEntryBase> item) => ((IDictionary<ConfigDefinition, ConfigEntryBase>)config).Add(item);

        public void Clear() => config.Clear();

        public bool Contains(KeyValuePair<ConfigDefinition, ConfigEntryBase> item) => config.Contains(item);

        void ICollection<KeyValuePair<ConfigDefinition, ConfigEntryBase>>.CopyTo(KeyValuePair<ConfigDefinition, ConfigEntryBase>[] array, int arrayIndex) => ((IDictionary<ConfigDefinition, ConfigEntryBase>)config).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<ConfigDefinition, ConfigEntryBase>>.Remove(KeyValuePair<ConfigDefinition, ConfigEntryBase> item) => ((IDictionary<ConfigDefinition, ConfigEntryBase>)config).Remove(item);

        public bool ContainsKey(ConfigDefinition key) => config.ContainsKey(key);

        bool IDictionary<ConfigDefinition, ConfigEntryBase>.TryGetValue(ConfigDefinition key, out ConfigEntryBase value) => ((IDictionary<ConfigDefinition, ConfigEntryBase>)config).TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<ConfigDefinition, ConfigEntryBase>> GetEnumerator() => config.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)config).GetEnumerator();

        public event EventHandler ConfigReloaded { add => config.ConfigReloaded += value; remove => config.ConfigReloaded -= value; }
        public event EventHandler<SettingChangedEventArgs> SettingChanged { add => config.SettingChanged += value; remove => config.SettingChanged -= value; }

    }
}
