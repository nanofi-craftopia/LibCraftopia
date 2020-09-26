using BepInEx.Logging;
using HarmonyLib;
using Oc;
using Oc.Item.UI;
using Oc.Missions;
using Oc.Skills;
using Oc.Statistical;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LibCraftopia.Loading
{
    [HarmonyPatch]
    public class LoadingPatch
    {
        private static bool isUIInventoryStarted = false;

        private static Queue<Action> startedManagers = new Queue<Action>();

        [HarmonyPatch(typeof(OcGameSceneTransformManager), "IELoadScenesAsync")]
        [HarmonyPostfix]
        static IEnumerator PostfixIELoadScenesAsync(IEnumerator original, bool needsStabilization, string ____nextSceneName)
        {
            while (original.MoveNext())
            {
                yield return original.Current;
                var scene = SceneManager.GetSceneByName(____nextSceneName);
                if (scene.isLoaded)
                {
                    break;
                }
            }
            if (____nextSceneName != "OcScene_Home")
            {
                Logger.Inst.LogInfo("Mods' initialization procedures start");
                var loaders = LoadingManager.Inst.InvokeInitialize(needsStabilization);
                while (loaders.MoveNext())
                {
                    yield return loaders.Current;
                }
                Logger.Inst.LogInfo("Mods' initialization procedures end");
            }
            yield return new WaitForEndOfFrame();
            while (startedManagers.Count > 0)
            {
                startedManagers.Dequeue()();
                yield return new WaitForEndOfFrame();
            }
            while (original.MoveNext())
            {
                yield return original.Current;
            }
            if (____nextSceneName != "OcScene_Home")
            {
                Logger.Inst.LogInfo("Mods' after loaded procedures start");
                var loaders = LoadingManager.Inst.InvokeAfterLoad(needsStabilization);
                while (loaders.MoveNext())
                {
                    yield return loaders.Current;
                }
                Logger.Inst.LogInfo("Mods' after loaded procedures end");
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

        [HarmonyPatch(typeof(OcItemUI_InventoryMng), "Start")]
        [HarmonyReversePatch]
        static void UIInventoryMngStart(OcItemUI_InventoryMng self)
        {
            self.LoadSaveData();
            OcPlMaster.Inst.LoadSaveData();
        }

        [HarmonyPatch(typeof(OcItemUI_InventoryMng), "Start")]
        [HarmonyPrefix]
        static bool PrefixUIInventoryMngStart()
        {
            Logger.Inst.LogInfo("OcItemUI_InventoryMng.Start");
            startedManagers.Enqueue(() =>
            {
                Logger.Inst.LogInfo("Load inventory and player data start");
                UIInventoryMngStart(OcItemUI_InventoryMng.Inst);
                isUIInventoryStarted = false;
                Logger.Inst.LogInfo("Load inventory and player data end");
            });
            isUIInventoryStarted = true;
            return false;
        }

        [HarmonyPatch(typeof(OcItemUI_InventoryMng), "Update")]
        [HarmonyPrefix]
        static bool PrefixUIInventoryMngUpdate()
        {
            return !isUIInventoryStarted;
        }


        [HarmonyPatch(typeof(OcPlMaster), "move")]
        [HarmonyPrefix]
        static bool PrefixPlMasterMove()
        {
            return !isUIInventoryStarted;
        }

        private static readonly MethodInfo SkillManagerLoadData = AccessTools.Method(typeof(OcSkillManager), "LoadData");
        [HarmonyPatch(typeof(OcSkillManager), "Start")]
        [HarmonyReversePatch]
        static void SkillManagerStart(OcSkillManager self)
        {
            SkillManagerLoadData.Invoke(self, new object[] { });
        }

        [HarmonyPatch(typeof(OcSkillManager), "Start")]
        [HarmonyPrefix]
        static bool PrefixSkillManagerStart()
        {
            Logger.Inst.LogInfo("OcSkillManager.Start");
            startedManagers.Enqueue(() =>
            {
                Logger.Inst.LogInfo("Load skill start");
                SkillManagerStart(OcSkillManager.Inst);
                Logger.Inst.LogInfo("Load skill end");
            });
            return false;
        }

        private static readonly MethodInfo MissionManagerLoadData = AccessTools.Method(typeof(OcMissionManager), "LoadData");
        private static readonly MethodInfo MissionManagerCalcMissionRewardStatus = AccessTools.Method(typeof(OcMissionManager), "CalcMissionRewardStatus");
        [HarmonyPatch(typeof(OcMissionManager), "Start")]
        [HarmonyReversePatch]
        static void MissionManagerStart(OcMissionManager self)
        {
            if (OcGameSceneShareSingleton.IsOpening)
            {
                return;
            }
            MissionManagerLoadData.Invoke(self, new object[] { });
            MissionManagerCalcMissionRewardStatus.Invoke(self, new object[] { });
            ref var dataLoaded = ref AccessTools.FieldRefAccess<OcMissionManager, bool>(self, "_dataLoaded");
            dataLoaded = true;
        }
        [HarmonyPatch(typeof(OcMissionManager), "Start")]
        [HarmonyPrefix]
        static bool PrefixMissionManagerStart()
        {
            Logger.Inst.LogInfo("OcMissionManager.Start");
            startedManagers.Enqueue(() =>
            {
                Logger.Inst.LogInfo("Load mission start");
                MissionManagerStart(OcMissionManager.Inst);
                Logger.Inst.LogInfo("Load misson end");
            });
            return false;
        }

        private static readonly MethodInfo UIHUDMissionRefreshNewMission = AccessTools.Method(typeof(OcUI_HUDMissionSheetHandler), "RefreshNewMission");
        [HarmonyPatch(typeof(OcUI_HUDMissionSheetHandler), "Start")]
        [HarmonyReversePatch]
        static void UIHUDMissionStart(OcUI_HUDMissionSheetHandler self)
        {
            UIHUDMissionRefreshNewMission.Invoke(self, new object[] { });
        }
        [HarmonyPatch(typeof(OcUI_HUDMissionSheetHandler), "Start")]
        [HarmonyPrefix]
        static bool PrefixUIHUDMissionStart()
        {
            Logger.Inst.LogInfo("OcUI_HUDMissionSheetHandler.Start");
            startedManagers.Enqueue(() =>
            {
                Logger.Inst.LogInfo("Load HUD mission start");
                UIHUDMissionStart(OcUI_HUDMissionSheetHandler.Inst);
                Logger.Inst.LogInfo("Load HUD mission end");
            });
            return false;
        }

        [HarmonyPatch(typeof(StatisticalDataManager), "Start")]
        [HarmonyReversePatch]
        static void StatisticalManagerStart(StatisticalDataManager self)
        {
            if (OcGameSceneShareSingleton.IsOpening)
            {
                return;
            }
            self.LoadData();
            ref var dataLoaded = ref AccessTools.FieldRefAccess<StatisticalDataManager, bool>(self, "_dataLoaded");
            dataLoaded = true;
        }

        [HarmonyPatch(typeof(StatisticalDataManager), "Start")]
        [HarmonyPrefix]
        static bool PrefixStatisticalManagerStart()
        {
            Logger.Inst.LogInfo("StatisticalDataManager.Start");
            startedManagers.Enqueue(() =>
            {
                Logger.Inst.LogInfo("Load statistical data start");
                StatisticalManagerStart(StatisticalDataManager.Inst);
                Logger.Inst.LogInfo("Load statistical data end");
            });
            return false;
        }
    }
}
