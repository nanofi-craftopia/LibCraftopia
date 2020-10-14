using BepInEx;
using HarmonyLib;
using System;

namespace LibCraftopia.Chat
{
    [BepInPlugin("com.craftopia.mod.LibCraftopiaChat", "LibCraftopia.Chat", ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch + "." + ThisAssembly.Git.Commits)]

    public class LibCraftopiaChat: BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.craftopia.mod.LibCraftopiaChat");
            harmony.PatchAll(typeof(ChatCommandPatch));
            this.gameObject.AddComponent<Logger>().Init(Logger);
        }

        void Start()
        {
            this.gameObject.AddComponent<ChatCommandManager>();
        }

    }
}
