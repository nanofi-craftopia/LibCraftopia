using HarmonyLib;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Item
{
    public class ItemFamilyList
    {
        private ItemManager manager;
        internal ItemFamilyList(ItemManager manager)
        {
            this.manager = manager;
        }
        public SoItemFamilyList Original => AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(manager.Original, "SoItemFamilyList");
        public ref SoItemFamily[] All => ref AccessTools.FieldRefAccess<SoDataList<SoItemFamilyList, SoItemFamily>, SoItemFamily[]>(Original, "all");
    }
}
