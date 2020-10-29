using EnhancedScrollerDemos.MainMenu;
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
using System.IO;
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
            yield return Task.Run(() =>
            {
                var items = elements.ToArray();
                var all = items.Select(e => (ItemData)e).ToArray();
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
                setupTreasureProb(items);
            }).LogError().AsCoroutine();
        }

        private void setupTreasureProb(IList<Item> elements)
        {
            var itemManager = OcItemDataMng.Inst;
            var itemList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(itemManager, "SoItemDataList");
            var probSums = AccessTools.FieldRefAccess<SoItemDataList, float[]>(itemList, "rarityChestProbSums");
            for (int i = 0; i < ItemHelper.Inst.MaxRarity; i++)
            {
                ref var probs = ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(itemList, $"rarity{i + 1}ChestProbs");
                probs = new float[elements.Count];
                float sum = 0;
                for (int j = 0; j < elements.Count; j++)
                {
                    var item = elements[j];
                    if (item.ProbsInTreasureBox == null) continue;
                    var p = item.ProbsInTreasureBox[i];
                    if (!(p > 0)) continue;
                    probs[j] = p;
                    sum += p;
                }
                probSums[i] = sum;
            }
        }

        public IEnumerator Init(Registry<Item> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var itemList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(itemManager, "SoItemDataList");
            var chestProbs = new List<float[]>();
            for (int i = 0; i < ItemHelper.Inst.MaxRarity; i++)
            {
                var p = AccessTools.FieldRefAccess<SoItemDataList, float[]>(itemList, $"rarity{i + 1}ChestProbs");
                chestProbs.Add(p);
            }
            yield return registry.RegisterVanillaElements(itemList.GetAll().Select((e, i) =>
            {
                var item = (Item)e;
                item.ProbsInTreasureBox = chestProbs.Select(p => p[i]).ToArray();
                return item;
            }), item =>
            {
                string key = LocalizationHelper.Inst.GetItemDisplayName(item.Id, LocalizationHelper.English)?.ToValidKey();
                if (key == null)
                {
                    key = item.Id.ToString();
                }
                else if (!item.IsEnabled)
                {
                    key += $"#{item.Id}";
                }
                return key;

            }, item => LocalizationHelper.Inst.GetItemDisplayName(item.Id, LocalizationHelper.Japanese)).AsCoroutine();
        }

        public void OnRegister(string key, int id, Item value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
