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

        public bool IsGameDependent => false;

        public IEnumerator Apply(ICollection<Enchant> elements)
        {
            var task = Task.Run(() =>
            {
                var all = elements.Select(e => (SoEnchantment)e).ToArray();
                AccessTools.FieldRefAccess<SoDataList<SoEnchantDataList, SoEnchantment>, SoEnchantment[]>(OcResidentData.EnchantDataList, "all") = all;
                setupTreasureProb(elements);
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
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void setupTreasureProb(ICollection<Enchant> elements)
        {
            var enchantList = OcResidentData.EnchantDataList;
            var maxId = elements.Select(e => e.Id).Max();
            var probSums = AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(enchantList, "rarityChestProbSums");
            for (int i = 0; i < EnchantHelper.Inst.MaxRarity; i++)
            {
                ref var probs = ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(enchantList, $"rarity{i + 1}ChestProbs");
                var origin = probs;
                probs = new float[maxId + 1];
                origin.CopyTo(probs, 0);
                float sum = 0;
                foreach (var enchant in elements.Where(e => e.ProbInTreasureBox != null))
                {
                    var prob = enchant.ProbInTreasureBox[i];
                    probs[enchant.Id] = prob;
                    sum += prob;
                }
                probSums[i] += sum;
            }
        }

        public IEnumerator Init(Registry<Enchant> registry)
        {
            var task = Task.Run(() =>
            {
                var enchantList = OcResidentData.EnchantDataList;
                foreach (var enchant in enchantList.GetAll())
                {
                    var key = LocalizationHelper.Inst.GetEnchantDisplayName(enchant.ID, LocalizationHelper.English)?.ToValidKey() ?? enchant.ID.ToString();
                    // DisplayName conflicts
                    if (key == "Test" || key == "Dried" || key.IsNullOrEmpty())
                    {
                        var jpName = LocalizationHelper.Inst.GetEnchantDisplayName(enchant.ID, LocalizationHelper.Japanese);
                        Logger.Inst.LogWarning($"Confliction: {enchant.ID}, {key}, {jpName}");
                        key += enchant.ID.ToString();
                    }
                    //
                    registry.RegisterVanilla(key, enchant);
                }
            }).LogError();
            while (!task.IsCompleted && !task.IsCanceled)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void OnRegister(string key, int id, Enchant value)
        {
        }

        public void OnUnregister(string key, int id)
        {
        }
    }
}
