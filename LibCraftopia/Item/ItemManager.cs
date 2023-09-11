using HarmonyLib;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Item
{
    public class ItemManager
    {
        private static ItemManager instance;
        public static ItemManager Inst
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemManager();
                }
                return instance;
            }
        }

        private ItemManager()
        {
            ItemList = new ItemList(this);
            ItemFamilyList = new ItemFamilyList(this);
        }

        public OcItemDataMng Original => OcItemDataMng.Inst;
        public ItemData EmptyData => Original.EmptyData;
        public ItemList ItemList { get; private set; }
        public ItemFamilyList ItemFamilyList { get; private set; }

        internal void InvokeOnUnityAwake()
        {
            AccessTools.FieldRefAccess<OcItemDataMng, Oc.BidirectionalDictionary<int, int>>(Original, "_BlueprintToRecipeDic").Clear();
            AccessTools.Method(typeof(OcItemDataMng), "OnUnityAwake").Invoke(Original, new object[] { });
        }
    }
}
