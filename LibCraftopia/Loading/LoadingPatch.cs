using BepInEx.Logging;
using HarmonyLib;
using Oc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Loading
{
    public class LoadingPatch
    {
        [HarmonyPatch(typeof(OcGameSceneTransformManager), "IELoadScenesAsync")]
        [HarmonyPostfix]
        static IEnumerator PostfixIELoadScenesAsync(IEnumerator original, bool needsStabilization)
        {
            Logger.Inst.LogInfo("Mods' loading procedures start");
            var loaders = LoadingManager.Inst.OnLoadScene(needsStabilization);
            while (loaders.MoveNext())
            {
                yield return loaders.Current;
            }
            Logger.Inst.LogInfo("Mods' loading procedures end");
            while (original.MoveNext())
            {
                yield return original.Current;
            }
        }

        [HarmonyPatch(typeof(OcGameMng), "UnloadGameScene")]
        [HarmonyPostfix]
        static IEnumerator PostfixUnloadGameScene(IEnumerator original)
        {

            while (original.MoveNext())
            {
                yield return original.Current;
            }
            LoadingManager.Inst.OnGameSceneUnloaded();
        }
    }
}
