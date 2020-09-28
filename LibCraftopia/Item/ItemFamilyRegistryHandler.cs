using HarmonyLib;
using LibCraftopia.Helper;
using LibCraftopia.Registry;
using LibCraftopia.Utils;
using Oc;
using Oc.Item;
using Oc.Item.UI;
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
        public int UserMinId { get; }
        public bool IsGameDependent => false;

        public ItemFamilyRegistryHandler()
        {
            UserMinId = Config.Inst.Bind("Item", "ItemFamilyMinUserId", 1000000, "The minimum id jof an item item family added by mod.").Value;
        }

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
            OcItemUI_CraftMng.Inst.UpdatePlayerCarftableItem();
        }

        public IEnumerator Init(Registry<ItemFamily> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var familyList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(itemManager, "SoItemFamilyList");
            var task = Task.Run(() =>
            {
                var all = familyList.GetAll();
                var counts = new Dictionary<string, int>();
                var list = new List<Tuple<string, SoItemFamily>>(all.Length);
                foreach (var family in all)
                {
                    var key = LocalizationHelper.Inst.GetItemFamily(family.FamilyId, LocalizationHelper.English)?.ToValidKey() ?? family.FamilyId.ToString();
                    counts.Increment(key);
                    list.Add(Tuple.Create(key, family));
                }
                foreach (var tuple in list)
                {
                    var key = tuple.Item1;
                    var family = tuple.Item2;
                    if (counts[key] > 1)
                    {
                        var jpName = LocalizationHelper.Inst.GetItemFamily(family.FamilyId, LocalizationHelper.Japanese);
                        Logger.Inst.LogWarning($"Confliction: {family.FamilyId}, {key}, {jpName}");
                        key += family.FamilyId.ToString();
                    }
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
