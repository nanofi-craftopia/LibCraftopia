using BepInEx;
using HarmonyLib;
using LibCraftopia.Chat;
using LibCraftopia.Enchant;
using LibCraftopia.Helper;
using LibCraftopia.Hook;
using LibCraftopia.Loading;
using LibCraftopia.Registry;
using System;
using System.Reflection;

namespace LibCraftopia
{
    [BepInPlugin("com.craftopia.mod.LibCraftopia", "LibCraftopia", ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch + "." + ThisAssembly.Git.Commits)]
    public class LibCraftopia : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.craftopia.mod.LibCraftopia");
            harmony.PatchAll(typeof(GlobalHook));
            harmony.PatchAll(typeof(LoadingPatch));
            harmony.PatchAll(typeof(OcItemDropperPatch));
            harmony.PatchAll(typeof(ChatCommandPatch));
            this.gameObject.AddComponent<Logger>().Init(Logger);
        }


        void Start()
        {
            this.gameObject.AddComponent<LoadingManager>();
            this.gameObject.AddComponent<RegistryManager>();
            this.gameObject.AddComponent<EnchantHelper>();
            this.gameObject.AddComponent<ItemHelper>();
            this.gameObject.AddComponent<LocalizationHelper>();
            this.gameObject.AddComponent<ChatCommandManager>();
        }

    }
}
