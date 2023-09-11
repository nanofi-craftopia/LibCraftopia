using Cysharp.Threading.Tasks;
using HarmonyLib;
using LibCraftopia.Localization;
using LibCraftopia.Registry;
using LibCraftopia.Utils;
using Oc;
using Oc.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LibCraftopia.Enchant
{
    public class EnchantRegistryHandler : IRegistryHandler<Enchant>
    {
        public int MaxId => short.MaxValue;

        public int MinId => 0;
        public int UserMinId { get; }

        public EnchantRegistryHandler()
        {
            UserMinId = Config.Inst.Bind("Enchant", "EnchantMinUserId", 15000, "The minimum id of an enchant added by mod.").Value;
        }

        public UniTask Apply(ICollection<Enchant> elements)
        {
            return UniTask.RunOnThreadPool(() =>
           {
               var enchants = elements.ToArray();
               var all = enchants.Select(e => (SoEnchantment)e).ToArray();
               EnchantList.Inst.All = all;
               setupTreasureProb(enchants);
           }).LogError();
        }

        private void setupTreasureProb(IList<Enchant> elements)
        {
            var maxId = elements.Select(e => e.Id).Max();
            var probSums = EnchantList.Inst.RarityChestProbSums;
            for (int i = 0; i < EnchantList.Inst.MaxRarity; i++)
            {
                ref var probs = ref EnchantList.Inst.RefRarityChestProbs(i);
                probs = new float[elements.Count];
                float sum = 0;
                for (int j = 0; j < elements.Count; j++)
                {
                    var enchant = elements[j];
                    if (enchant.ProbsInTreasureBox == null) continue;
                    var p = enchant.ProbsInTreasureBox[i];
                    if (!(p > 0)) continue;
                    probs[j] = p;
                    sum += p;

                }
                probSums[i] = sum;
            }
        }

        public async UniTask Init(Registry<Enchant> registry)
        {
            var chestProbs = new List<float[]>();
            for (int i = 0; i < EnchantList.Inst.MaxRarity; i++)
            {
                chestProbs.Add(EnchantList.Inst.RefRarityChestProbs(i));
            }
            await registry.RegisterVanillaElements(EnchantList.Inst.All.Select((e, i) =>
            {
                var enchant = (Enchant)e;
                enchant.ProbsInTreasureBox = chestProbs.Select(p => p[i]).ToArray();
                return enchant;
            }),
                enchant => LocalizationHelper.Inst.GetEnchantDisplayName(enchant.Id, LocalizationHelper.English)?.ToValidKey() ?? enchant.Id.ToString(),
                enchant => LocalizationHelper.Inst.GetEnchantDisplayName(enchant.Id, LocalizationHelper.Japanese));
        }

        public void OnRegister(string key, int id, Enchant value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
