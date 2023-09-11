using Cysharp.Threading.Tasks;
using HarmonyLib;
using LibCraftopia.Localization;
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

        public UniTask Apply(ICollection<ItemFamily> elements)
        {
            return UniTask.RunOnThreadPool(() =>
            {
                var all = elements.Select(e => (SoItemFamily)e).ToArray();
                ItemManager.Inst.ItemFamilyList.All = all;
            }).LogError();
        }

        public UniTask Init(Registry<ItemFamily> registry)
        {
            return registry.RegisterVanillaElements(ItemManager.Inst.ItemFamilyList.All.Select(e => (ItemFamily)e),
               family => LocalizationHelper.Inst.GetItemFamily(family.Id, LocalizationHelper.English)?.ToValidKey() ?? family.Id.ToString(),
               family => LocalizationHelper.Inst.GetItemFamily(family.Id, LocalizationHelper.Japanese));
        }

        public void OnRegister(string key, int id, ItemFamily value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
