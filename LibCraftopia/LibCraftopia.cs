using BepInEx;
using HarmonyLib;
using LibCraftopia.Helper;
using LibCraftopia.Hook;
using System;

namespace LibCraftopia
{
    [BepInPlugin("com.craftopia.mod.LibCraftopia", "LibCraftopia", "0.1.0.0")]
    public class LibCraftopia : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.craftopia.mod.LibCraftopia");
            harmony.PatchAll(typeof(GlobalHook));
        }


        void Start()
        {
            this.gameObject.AddComponent<ItemHelper>();
            this.gameObject.AddComponent<LocalizationHelper>();
        }

    }
}
