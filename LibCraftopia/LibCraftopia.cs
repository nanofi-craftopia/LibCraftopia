using BepInEx;
using HarmonyLib;
using LibCraftopia.Chat;
using LibCraftopia.Enchant;
using LibCraftopia.Helper;
using LibCraftopia.Hook;
using System;

namespace LibCraftopia
{
    [BepInPlugin("com.craftopia.mod.LibCraftopia", "LibCraftopia", "0.1.3.0")]
    public class LibCraftopia : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.craftopia.mod.LibCraftopia");
            harmony.PatchAll(typeof(GlobalHook));
            harmony.PatchAll(typeof(OcItemDropperPatch));
            harmony.PatchAll(typeof(ChatCommandPatch));
            this.gameObject.AddComponent<Logger>().Init(Logger);
        }


        void Start()
        {
            this.gameObject.AddComponent<EnchantHelper>();
            this.gameObject.AddComponent<ItemHelper>();
            this.gameObject.AddComponent<LocalizationHelper>();
            this.gameObject.AddComponent<ChatCommandManager>();
        }

    }
}
