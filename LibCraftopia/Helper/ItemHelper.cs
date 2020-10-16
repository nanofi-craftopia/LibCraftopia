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
            MaxRarity = AccessTools.StaticFieldRefAccess<SoItemDataList, int>("MaxRarity");

            itemDataMngTraverse = new Traverse(OcItemDataMng.Inst);
            itemList = itemDataMngTraverse.Field<SoItemDataList>("SoItemDataList").Value;
            itemListTraverse = new Traverse(itemList);

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

        public int MaxRarity { get; private set; }

        public ItemHandlerSO DefaultHandler { get; private set; }
        public ItemHandlerSO EquipHandler { get; private set; }
        public ItemHandlerSO PotionHandler { get; private set; }
        public ItemHandlerSO SkillHandler { get; private set; }
        public ItemHandlerSO BreedSeedHandler { get; private set; }
        public ItemHandlerSO UseActHandler { get; private set; }


    }
}

