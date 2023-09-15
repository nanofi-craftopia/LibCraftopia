using BepInEx;
using HarmonyLib;
using LibCraftopia.Enchant;
using LibCraftopia.Localization;
using LibCraftopia.Item;
using LibCraftopia.Registry;
using Oc;
using System;
using System.Collections;
using System.Reflection;
using LibCraftopia.Startup;
using LibCraftopia.Initialize;

namespace LibCraftopia
{
    [BepInPlugin(LibCraftopia.GUID, "LibCraftopia", ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch + "." + ThisAssembly.Git.Commits)]
    public class LibCraftopia : BaseUnityPlugin
    {
        public const string GUID = "com.craftopia.mod.LibCraftopia";

        void Awake()
        {
            this.gameObject.AddComponent<Config>().Init(Config);
            this.gameObject.AddComponent<Logger>().Init(Logger);
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            this.gameObject.AddComponent<StartupManager>();
            this.gameObject.AddComponent<LocalizationHelper>();
            this.gameObject.AddComponent<InitializeManager>();
            this.gameObject.AddComponent<RegistryManager>();
            this.gameObject.AddComponent<ItemAssetManager>();
        }

        private void Start()
        {
            RegistryManager.Inst.CreateRegistry(new ItemFamilyRegistryHandler());
            RegistryManager.Inst.CreateRegistry(new ItemRegistryHandler());
            RegistryManager.Inst.CreateRegistry(new EnchantRegistryHandler());
        }
    }
}
