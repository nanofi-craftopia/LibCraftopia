using HarmonyLib;
using LibCraftopia.Container;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Helper
{
    public class ItemHelper : SingletonMonoBehaviour<ItemHelper>
    {
        protected override void OnUnityAwake()
        {
            itemDataMngTraverse = new Traverse(OcItemDataMng.Inst);
            itemList = itemDataMngTraverse.Field<SoItemDataList>("SoItemDataList").Value;
            itemListTraverse = new Traverse(itemList);
            allItems = new CachedTraverse<ItemData[]>(itemListTraverse.Field("all"));
            allItems.Changed += updateLastItemId;
            familyList = itemDataMngTraverse.Field<SoItemFamilyList>("SoItemFamilyList").Value;
            familyListTraverse = new Traverse(familyList);
            allFamilies = new CachedTraverse<SoItemFamily[]>(familyListTraverse.Field("all"));
            allFamilies.Changed += updateLastFamilyId;

            DefaultHandler = itemListTraverse.Field<ItemHandlerSO>("defaultHandler").Value;
            EquipHandler = itemListTraverse.Field<ItemHandlerSO>("equipHandler").Value;
            PotionHandler = itemListTraverse.Field<ItemHandlerSO>("potionHandler").Value;
            SkillHandler = itemListTraverse.Field<ItemHandlerSO>("skillHandler").Value;
            BreedSeedHandler = itemListTraverse.Field<ItemHandlerSO>("breedSeedHandler").Value;
            UseActHandler = itemListTraverse.Field<ItemHandlerSO>("useActHandler").Value;
        }

        private Traverse itemDataMngTraverse;
        private SoItemDataList itemList;
        private Traverse itemListTraverse;
        private CachedTraverse<ItemData[]> allItems;
        private int lastId;
        private SoItemFamilyList familyList;
        private Traverse familyListTraverse;
        private CachedTraverse<SoItemFamily[]> allFamilies;
        private int lastFamilyId;

        public ItemHandlerSO DefaultHandler { get; private set; }
        public ItemHandlerSO EquipHandler { get; private set; }
        public ItemHandlerSO PotionHandler { get; private set; }
        public ItemHandlerSO SkillHandler { get; private set; }
        public ItemHandlerSO BreedSeedHandler { get; private set; }
        public ItemHandlerSO UseActHandler { get; private set; }


        private void updateLastItemId(CachedTraverse<ItemData[]> sender, ItemData[] oldValue, ItemData[] newValue)
        {
            lastId = Math.Max(lastId, newValue.Select(item => item.Id).Max());
        }

        private void updateLastFamilyId(CachedTraverse<SoItemFamily[]> sender, SoItemFamily[] oldValue, SoItemFamily[] newValue)
        {
            lastFamilyId = Math.Max(lastFamilyId, newValue.Select(item => item.FamilyId).Max());
        }

        [Obsolete]
        public ItemData[] AllItems
        {
            get { return allItems.Value; }
        }
        [Obsolete]
        public ItemData[] ValidItems
        {
            get { return itemDataMngTraverse.Field<ItemData[]>("validItemDataList").Value; }
        }
        [Obsolete]
        public ItemData[] AppearIngameItems
        {
            get { return itemDataMngTraverse.Field<ItemData[]>("appearIngameItemDataList").Value; }
        }
        [Obsolete]
        public SoItemFamily[] AllFamilies
        {
            get { return allFamilies.Value; }
        }

        [Obsolete]
        public int NewId()
        {
            allItems.Cache();
            return ++lastId;
        }

        [Obsolete]
        public void AddItems(params ItemData[] items)
        {
            AddItemsFromEnumerable(items);
        }

        [Obsolete]
        public void AddItemsFromEnumerable(IEnumerable<ItemData> items)
        {
            var itemList = items.ToList();
            var newAllItems = allItems.Value.Concat(itemList).ToArray();
            allItems.Value = newAllItems;
            var validItems = itemList.Where(item => item.IsEnabled);
            var validDataList = itemDataMngTraverse.Field<ItemData[]>("validItemDataList");
            validDataList.Value = validDataList.Value.Concat(validItems).ToArray();
            var appearIngameItems = itemList.Where(item => item.IsAppearIngame);
            var appearIngameDataList = itemDataMngTraverse.Field<ItemData[]>("appearIngameItemDataList");
            appearIngameDataList.Value = appearIngameDataList.Value.Concat(appearIngameItems).ToArray();
            addCraftableItems(appearIngameItems);
            addCraftBenchMap(appearIngameItems);
            addFamilyMap(appearIngameItems);
            addCategoryMap(appearIngameItems);
            // TODO: is it necessary to modify the OcItemDataMng._FilleDataList?
        }

        private void addCraftableItems(IEnumerable<ItemData> items)
        {
            // See OcItemDataMng.SetupCraftableItems
            var craftableItems = itemDataMngTraverse.Field<Dictionary<int, List<ItemData>>>("craftableItems").Value;
            foreach (var item in items)
            {
                if (item.IsAppearIngame)
                {
                    foreach (var material in item.MaterialData)
                    {
                        if (!craftableItems.ContainsKey(material))
                        {
                            craftableItems.Add(material, new List<ItemData>());
                        }
                        craftableItems[material].Add(item);
                    }
                }
            }

        }

        private void addCraftBenchMap(IEnumerable<ItemData> items)
        {
            // See OcItemDataMng.SetupCraftBenchMap
            var craftBenchMap = itemDataMngTraverse.Field<Dictionary<int, List<ItemData>>>("craftBenchMap").Value;
            int player_CRAFT_FAMILY_ID = ItemData.PLAYER_CRAFT_FAMILY_ID;
            int workbench_CRAFT_FAMILY_ID = ItemData.WORKBENCH_CRAFT_FAMILY_ID;
            foreach (var item in items)
            {
                if (item.IsCraftable && item.IsAppearIngame)
                {
                    int workBenchId = item.WorkBenchId;
                    if (!craftBenchMap.ContainsKey(workBenchId))
                    {
                        craftBenchMap.Add(workBenchId, new List<ItemData>());
                    }
                    craftBenchMap[workBenchId].Add(item);
                    if (workBenchId == player_CRAFT_FAMILY_ID)
                    {
                        if (!craftBenchMap.ContainsKey(workbench_CRAFT_FAMILY_ID))
                        {
                            craftBenchMap.Add(workbench_CRAFT_FAMILY_ID, new List<ItemData>());
                        }
                        craftBenchMap[workbench_CRAFT_FAMILY_ID].Add(item);
                    }
                }
            }
            foreach (int key in new List<int>(craftBenchMap.Keys))
            {
                craftBenchMap[key].Sort((x, y) => x.Id.CompareTo(y.Id));
            }
        }

        private void addFamilyMap(IEnumerable<ItemData> items)
        {
            // See OcItemDataMng.SetupFamilyMap
            var familyMap = itemDataMngTraverse.Field<Dictionary<int, List<ItemData>>>("familyMap").Value;
            foreach (ItemData item in items)
            {
                int familyId = item.FamilyId;
                if (!familyMap.ContainsKey(familyId))
                {
                    familyMap.Add(familyId, new List<ItemData>());
                }
                familyMap[familyId].Add(item);
            }

        }
        private void addCategoryMap(IEnumerable<ItemData> items)
        {
            // See OcItemDataMng.SetupCategoryMap
            var categoryMap = itemDataMngTraverse.Field<Dictionary<int, List<ItemData>>>("categoryMap").Value;
            foreach (ItemData item in items)
            {
                int categoryId = item.CategoryId;
                if (!categoryMap.ContainsKey(categoryId))
                {
                    categoryMap.Add(categoryId, new List<ItemData>());
                }
                categoryMap[categoryId].Add(item);
            }

        }
        [Obsolete]
        public int NewFamilyId()
        {
            allFamilies.Cache();
            return ++lastFamilyId;
        }

        [Obsolete]
        public void AddItemFamilies(params SoItemFamily[] families)
        {
            AddItemFamiliesFromEnumerable(families);
        }

        [Obsolete]
        public void AddItemFamiliesFromEnumerable(IEnumerable<SoItemFamily> families)
        {
            var newFamilies = allFamilies.Value.Concat(families).ToArray();
            allFamilies.Value = newFamilies;
        }
    }
}

