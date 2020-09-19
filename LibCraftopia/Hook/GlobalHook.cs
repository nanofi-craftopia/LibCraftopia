using HarmonyLib;
using Oc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Hook
{
    public class GlobalHook
    {
        public static event Action OnGameStart;
        public static event Action OnGameScreenSetUpFinished;

        [HarmonyPatch(typeof(OcGameMng), "Start")]
        [HarmonyPostfix]
        static void OnGameMng_Start()
        {
            OnGameStart?.Invoke();
        }

        [HarmonyPatch(typeof(OcGameMng), "OnGameSceneSetUpFinish")]
        [HarmonyPostfix]
        static void OcGameMng_OnGameSceneSetUpFinish()
        {
            OnGameScreenSetUpFinished?.Invoke();
        }
    }
}
