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
            }).LogError().AsCoroutine();
        }

        public IEnumerator Init(Registry<Item> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var itemList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(itemManager, "SoItemDataList");
            yield return registry.RegisterVanillaElements(itemList.GetAll().Select(e => (Item)e), item =>
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
