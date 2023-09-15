using HarmonyLib;
using Oc;
using Oc.AssetLoadSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    [HarmonyPatch]
    internal class ItemAssetManagerPatch
    {
        private static Type typeAssetHandle = AccessTools.Inner(typeof(OcItemAssetMng), "AssetHandle");
        private static AccessTools.FieldRef<object, int> fieldItemId = AccessTools.FieldRefAccess<int>(typeAssetHandle, "<itemId>k__BackingField");

        static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeAssetHandle, "GetComponentInPrefab");
        }

        static void Prefix(object __instance, ref GameObject ___prefab, ref OcItemPrefab ___componentInPrefab)
        {
            if (___componentInPrefab != null) return;
            var id = fieldItemId(__instance);
            ___prefab = ItemAssetManager.Inst.LoadItemPrefab(id);
            if ( ___prefab != null )
            {
                ___componentInPrefab = ___prefab.GetComponent<OcItemPrefab>();
            }
        }
    }
}
