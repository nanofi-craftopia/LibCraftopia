using HarmonyLib;
using Oc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Hook
{
    public class GlobalHook 
    {
        public static event Action OnGameScreenSetUpFinished;

        [HarmonyPatch(typeof(OcGameMng), "OnGameSceneSetUpFinish")]
        [HarmonyPostfix]
        public static void OcGameMng_OnGameSceneSetUpFinish()
        {
            OnGameScreenSetUpFinished?.Invoke();
        }
    }
}
