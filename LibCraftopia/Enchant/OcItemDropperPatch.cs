using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Oc;
using Oc.Item;
using LibCraftopia.Helper;

namespace LibCraftopia.Enchant
{
    
    class OcItemDropperPatch
    {
        [HarmonyPatch(typeof(OcItemDropper), "TryAttachEnchant")]
        [HarmonyPostfix]
        static void Postfix(OcItemDropper __instance, ref OcItem item, SoEnemyData EnemyData)
        {
            var isEnemy = __instance.GetComponent<Oc.Em.OcEm>() != null;
            if (isEnemy)
            {
                HandleEnemy(ref item, EnemyData);
                return;
            }

            var obj = __instance.GetComponent<Oc.OcStaticObj>();
            if (obj.StaticObjType == OcStaticObjType.Tree)
            {
                HandleTree(ref item);
            }
            else if (obj.StaticObjType == OcStaticObjType.Stone)
            {
                HandleStone(ref item);
            }

        }

        private static void HandleEnemy(ref OcItem item, SoEnemyData EnemyData)
        {
            foreach (KeyValuePair<int, float> pair in EnchantHelper.Inst.UnspecifiedEnemyDrop)
            {
                TryAddEnchant(pair, ref item);
            }
            if (EnchantHelper.Inst.SpecifiedEnemyDrop.ContainsKey(EnemyData.ID))
            {
                foreach (KeyValuePair<int, float> pair in EnchantHelper.Inst.SpecifiedEnemyDrop[EnemyData.ID])
                {
                    TryAddEnchant(pair, ref item);
                }
            }
        }

        private static void HandleTree(ref OcItem item)
        {
            foreach (KeyValuePair<int, float> pair in EnchantHelper.Inst.TreeRandomDrop)
            {
                TryAddEnchant(pair, ref item);
            }
        }

        private static void HandleStone(ref OcItem item)
        {
            foreach (KeyValuePair<int, float> pair in EnchantHelper.Inst.StoneRandomDrop)
            {
                TryAddEnchant(pair, ref item);
            }
        }

        private static bool TryAddEnchant(KeyValuePair<int, float> pair, ref OcItem item)
        {
            var v = UnityEngine.Random.value;
            if (v < pair.Value)
            {
                SoEnchantment byId_OrNull = OcResidentData.EnchantDataList.GetById_OrNull(pair.Key);
                if (byId_OrNull != null)
                {
                    return item.TryAttachEnchant(byId_OrNull, false);
                }
            }
            return false;
        }
    }
}
