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
            yield return Task.Run(() =>
            {
                var all = elements.Select(e => (SoItemFamily)e).ToArray();
                var itemManager = OcItemDataMng.Inst;
                var familyList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(itemManager, "SoItemFamilyList");
                AccessTools.FieldRefAccess<SoDataList<SoItemFamilyList, SoItemFamily>, SoItemFamily[]>(familyList, "all") = all;
            }).LogError().AsCoroutine();
            OcItemUI_CraftMng.Inst.UpdatePlayerCarftableItem();
        }

        public IEnumerator Init(Registry<ItemFamily> registry)
        {
            var itemManager = OcItemDataMng.Inst;
            var familyList = AccessTools.FieldRefAccess<OcItemDataMng, SoItemFamilyList>(itemManager, "SoItemFamilyList");
            yield return registry.RegisterVanillaElements(familyList.GetAll().Select(e => (ItemFamily)e), 
                family => LocalizationHelper.Inst.GetItemFamily(family.Id, LocalizationHelper.English)?.ToValidKey() ?? family.Id.ToString(), 
                family => LocalizationHelper.Inst.GetItemFamily(family.Id, LocalizationHelper.Japanese)).AsCoroutine();
        }

        public void OnRegister(string key, int id, ItemFamily value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
