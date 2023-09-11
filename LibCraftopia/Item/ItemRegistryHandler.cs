using Cysharp.Threading.Tasks;
using HarmonyLib;
using I2.Loc;
using LibCraftopia.Localization;
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

        public ItemRegistryHandler()
        {
            UserMinId = Config.Inst.Bind("Item", "ItemMinUserId", 15000, "The minimum id of an item added by mod.").Value;
        }

        public UniTask Apply(ICollection<Item> elements)
        {
            return UniTask.RunOnThreadPool(() =>
            {
                var items = elements.ToArray();
                var all = items.Select(e => (ItemData)e).ToArray();
                ItemManager.Inst.ItemList.All = all;
                setupTreasureProb(items);
                ItemManager.Inst.InvokeOnUnityAwake();
            }).LogError();
        }

        private void setupTreasureProb(IList<Item> elements)
        {
            var probSums = ItemManager.Inst.ItemList.RarityChestProbSums;
            for (int i = 0; i < ItemManager.Inst.ItemList.MaxRank; i++)
            {
                ref var probs = ref ItemManager.Inst.ItemList.RefRarityChestProbs(i + 1);
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

        public async UniTask Init(Registry<Item> registry)
        {
            var chestProbs = new List<float[]>();
            for (int i = 0; i < ItemManager.Inst.ItemList.MaxRank; i++)
            {
                chestProbs.Add(ItemManager.Inst.ItemList.RefRarityChestProbs(i + 1));
            }
            await registry.RegisterVanillaElements(ItemManager.Inst.ItemList.All.Select((e, i) =>
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

            }, item => LocalizationHelper.Inst.GetItemDisplayName(item.Id, LocalizationHelper.Japanese));
        }

        public void OnRegister(string key, int id, Item value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
