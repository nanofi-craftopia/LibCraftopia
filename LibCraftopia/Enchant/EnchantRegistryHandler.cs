using HarmonyLib;
using LibCraftopia.Helper;
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
        public int MaxId => byte.MaxValue;

        public int MinId => 0;
        public int UserMinId { get; }

        public bool IsGameDependent => false;

        public EnchantRegistryHandler()
        {
            UserMinId = Config.Inst.Bind("Enchant", "EnchantMinUserId", 200, "The minimum id of an enchant added by mod.").Value;
        }

        public IEnumerator Apply(ICollection<Enchant> elements)
        {
            yield return Task.Run(() =>
            {
                var all = elements.Select(e => (SoEnchantment)e).ToArray();
                AccessTools.FieldRefAccess<SoDataList<SoEnchantDataList, SoEnchantment>, SoEnchantment[]>(OcResidentData.EnchantDataList, "all") = all;
                // setupTreasureProb(elements);
                foreach (var enchant in elements)
                {
                    if (enchant.ProbInRandomDrop > 0.0f)
                        EnchantHelper.Inst.UnspecifiedEnemyDrop[enchant.Id] = enchant.ProbInRandomDrop;
                    if (enchant.ProbInStoneDrop > 0.0f)
                        EnchantHelper.Inst.StoneRandomDrop[enchant.Id] = enchant.ProbInStoneDrop;
                    if (enchant.ProbInTreeDrop > 0.0f)
                        EnchantHelper.Inst.TreeRandomDrop[enchant.Id] = enchant.ProbInTreeDrop;
                    EnchantHelper.Inst.SpecifiedEnemyDrop[enchant.Id] = enchant.ProbInEnemyDrop;
                }
            }).LogError().AsCoroutine();
        }

        private void setupTreasureProb(ICollection<Enchant> elements)
        {
            var enchantList = OcResidentData.EnchantDataList;
            var maxId = elements.Select(e => e.Id).Max();
            var probSums = AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(enchantList, "rarityChestProbSums");
            for (int i = 0; i < EnchantHelper.Inst.MaxRarity; i++)
            {
                ref var probs = ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(enchantList, $"rarity{i}ChestProbs");
                probs = new float[elements.Count];
                float sum = 0;
                int idx = 0;
                foreach (var enchant in elements)
                {
                    if (enchant.ProbsInTreasureBox == null) continue;
                    var p = enchant.ProbsInTreasureBox[i];
                    if (!(p > 0)) continue;
                    probs[idx] = p;
                    sum += p;
                    idx++;
                }
                probSums[i] += sum;
            }
        }

        public IEnumerator Init(Registry<Enchant> registry)
        {
            var enchantList = OcResidentData.EnchantDataList;
            var all = enchantList.GetAll();
            var chestProbs = new List<float[]>();
            for (int i = 0; i < EnchantHelper.Inst.MaxRarity; i++)
            {
                var p = AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(enchantList, $"rarity{i}ChestProbs");
                chestProbs.Add(p);
            }
            yield return registry.RegisterVanillaElements(all.Select((e, i) =>
            {
                var enchant = (Enchant)e;
                enchant.ProbsInTreasureBox = chestProbs.Select(p => p[i]).ToArray();
                return enchant;
            }),
                enchant => LocalizationHelper.Inst.GetEnchantDisplayName(enchant.Id, LocalizationHelper.English)?.ToValidKey() ?? enchant.Id.ToString(),
                enchant => LocalizationHelper.Inst.GetEnchantDisplayName(enchant.Id, LocalizationHelper.Japanese)).AsCoroutine();
        }

        public void OnRegister(string key, int id, Enchant value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
