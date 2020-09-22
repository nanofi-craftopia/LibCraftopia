using HarmonyLib;
using LibCraftopia.Helper;
using LibCraftopia.Registry;
using LibCraftopia.Utils;
using Oc;
using Oc.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemFamilyRegistryHandler : IRegistryHandler<ItemFamily>
    {
        public int MaxId => int.MaxValue;

        public int MinId => 0;

        public bool IsGameDependent => false;

        public IEnumerator Apply(ICollection<ItemFamily> elements)
        {
            var task = Task.Run(() =>
            {
                var all = elements.Select(e => (SoItemFamily)e).ToArray();
                var itemManager = OcItemDataMng.Inst;
                var familyList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(itemManager, "SoItemFamilyList");
                AccessTools.FieldRefAccess<SoDataList<SoItemFamilyList, SoItemFamily>, SoItemFamily[]>(familyList, "all") = all;
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public IEnumerator Init(Registry<ItemFamily> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var familyList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(itemManager, "SoItemFamilyList");
            var task = Task.Run(() =>
            {
                foreach (var family in familyList.GetAll())
                {
                    var key = LocalizationHelper.Inst.GetItemFamily(family.FamilyId, LocalizationHelper.English)?.ToValidKey() ?? family.FamilyId.ToString();
                    var jpName = LocalizationHelper.Inst.GetItemFamily(family.FamilyId, LocalizationHelper.Japanese);
                    // DisplayName conflicts
                    if (key == "OneHandedSword" || key == "TwoHandedWeapon" || key == "Leftover" || key.IsNullOrEmpty())
                    {
                        Logger.Inst.LogWarning($"Possible confliction: {family.FamilyId}, {key}, {jpName}");
                        key += family.FamilyId.ToString();
                    }
                    // 
                    registry.RegisterVanilla(key, family);
                }
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void OnRegister(string key, int id, ItemFamily value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
