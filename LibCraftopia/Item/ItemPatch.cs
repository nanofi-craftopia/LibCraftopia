using HarmonyLib;
using LibCraftopia.Registry;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Item
{
    [HarmonyPatch]
    public class ItemPatch
    {
        [HarmonyPatch(typeof(OcPl), "checkAct_Put")]
        [HarmonyPrefix]
        static bool PrefixOcPlcheckAct_Put(OcPlEquipCtrl ____EquipCtrl)
        {
            try
            {
                OcItem itemData = ____EquipCtrl.getWpMain()?.Item;
                if (itemData != null)
                {
                    var id = itemData.Id;
                    var registry = RegistryManager.Inst.GetRegistry<Item>();
                    var item = registry.GetElementById(id);
                    var result = item?.PutHandler?.IsPuttable();
                    return !result.HasValue || result.Value;
                }
            }
            catch (Exception e)
            {
                Logger.Inst.LogException(e);
            }
            return true;
        }

        [HarmonyPatch(typeof(OcInstallObjMng), "Instantiate_Dissolver_ForMaster")]
        [HarmonyPostfix]
        static void PostfixOcInstallObjMngInstantiate_Dissolver_ForMaster(OcItem __1, OcPlEquip __result)
        {
            try
            {
                var id = __1.Id;
                if (!RegistryManager.Inst.Initialized)
                {
                    Logger.Inst.LogInfo($"Put an item before initialized registry: {__1.DisplayName}({__1.Id})");
                    return;
                }
                var registry = RegistryManager.Inst.GetRegistry<Item>();
                var item = registry.GetElementById(id);
                item?.PutHandler?.OnPut(__result);

            }
            catch (Exception e)
            {
                Logger.Inst.LogException(e);
            }
        }

        [HarmonyPatch(typeof(OcInstallObjMng), "killInstallObj", typeof(OcPlEquip))]
        [HarmonyPostfix]
        static void PostfixOcInstallObjMngkillInstallObj(OcPlEquip eq)
        {
            try
            {
                var id = eq.Item.Id;
                var registry = RegistryManager.Inst.GetRegistry<Item>();
                var item = registry.GetElementById(id);
                item?.PutHandler?.OnKilled(eq);
            }
            catch (Exception e)
            {
                Logger.Inst.LogException(e);
            }
        }
    }
}
