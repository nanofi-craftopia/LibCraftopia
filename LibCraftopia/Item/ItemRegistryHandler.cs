using HarmonyLib;
using I2.Loc;
using LibCraftopia.Helper;
using LibCraftopia.Registry;
using LibCraftopia.Utils;
using Oc;
using Oc.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemRegistryHandler : IRegistryHandler<Item>
    {
        public int MaxId => (int)short.MaxValue;

        public int MinId => 0;
        public int UserMinId { get; }

        public bool IsGameDependent => false;

        public ItemRegistryHandler()
        {
            UserMinId = Config.Inst.Bind("Item", "ItemMinUserId", 15000, "The minimum id of an item added by mod.").Value;
        }

        public IEnumerator Apply(ICollection<Item> elements)
        {
            var task = Task.Run(() =>
            {
                var all = elements.Select(e => (ItemData)e).ToArray();
                var valid = all.Where(e => e.IsEnabled).ToArray();
                var appear = valid.Where(e => e.IsAppearIngame).ToArray();
                var itemManager = OcItemDataMng.Inst;
                var itemList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(itemManager, "SoItemDataList");
                AccessTools.FieldRefAccess<SoDataList<SoItemDataList, ItemData>, ItemData[]>(itemList, "all") = all;
                AccessTools.FieldRefAccess<OcItemDataMng, ItemData[]>(itemManager, "validItemDataList") = valid;
                AccessTools.FieldRefAccess<OcItemDataMng, ItemData[]>(itemManager, "appearIngameItemDataList") = appear;
                AccessTools.Method(typeof(OcItemDataMng), "SetupCraftableItems").Invoke(itemManager, new object[] { });
                AccessTools.Method(typeof(OcItemDataMng), "SetupCraftBenchMap").Invoke(itemManager, new object[] { });
                AccessTools.Method(typeof(OcItemDataMng), "SetupFamilyMap").Invoke(itemManager, new object[] { });
                AccessTools.Method(typeof(OcItemDataMng), "SetupCategoryMap").Invoke(itemManager, new object[] { });
                var fille = itemManager.GetFamilyItems(40712).OrderBy(e => e.Price).ToArray();
                AccessTools.FieldRefAccess<OcItemDataMng, ItemData[]>(itemManager, "_FilletDataList") = fille;
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public IEnumerator Init(Registry<Item> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var itemList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(itemManager, "SoItemDataList");
            var task = Task.Run(() =>
            {
                var all = itemList.GetAll();
                var counts = new Dictionary<string, int>();
                var list = new List<Tuple<string, ItemData>>(all.Length);
                foreach (var item in all)
                {
                    var key = item.IsEnabled ? LocalizationHelper.Inst.GetItemDisplayName(item.Id, LocalizationHelper.English)?.ToValidKey() ?? item.Id.ToString() : item.Id.ToString();
                    counts.Increment(key);
                    list.Add(Tuple.Create(key, item));
                }
                foreach (var tuple in list)
                {
                    var key = tuple.Item1;
                    var item = tuple.Item2;
                    if (counts[key] > 1)
                    {
                        var jpName = LocalizationHelper.Inst.GetItemDisplayName(item.Id, LocalizationHelper.Japanese);
                        Logger.Inst.LogWarning($"Confliction: {item.Id}, {key}, {jpName}");
                        key += item.Id.ToString();
                    }
                    registry.RegisterVanilla(key, item);
                }
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void OnRegister(string key, int id, Item value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
