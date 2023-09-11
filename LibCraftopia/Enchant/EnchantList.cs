using HarmonyLib;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Enchant
{
    public class EnchantList
    {
        private static EnchantList instance;
        public static EnchantList Inst
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnchantList();
                }
                return instance;
            }
        }

        private EnchantList()
        {
            MaxRarity = AccessTools.StaticFieldRefAccess<SoEnchantDataList, int>("MaxRarity");
        }

        public int MaxRarity { get; private set; }
        public SoEnchantDataList Original => OcResidentData.EnchantDataList;

        public ref SoEnchantment[] All => ref AccessTools.FieldRefAccess<SoDataList<SoEnchantDataList, SoEnchantment>, SoEnchantment[]>(Original, "all");

        public ref float[] RarityChestProbSums => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarityChestProbSums");
        public ref float[] Rarity0ChestProbs => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarity0ChestProbs");
        public ref float[] Rarity1ChestProbs => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarity1ChestProbs");
        public ref float[] Rarity2ChestProbs => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarity2ChestProbs");
        public ref float[] Rarity3ChestProbs => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarity3ChestProbs");
        public ref float[] Rarity4ChestProbs => ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(Original, "rarity4ChestProbs");
        public ref float[] RefRarityChestProbs(int rarity)
        {
            switch (rarity)
            {
                case 0: return ref Rarity0ChestProbs;
                case 1: return ref Rarity1ChestProbs;
                case 2: return ref Rarity2ChestProbs;
                case 3: return ref Rarity3ChestProbs;
                case 4: return ref Rarity4ChestProbs;
                default: throw new ArgumentOutOfRangeException(nameof(rarity));
            }
        }

    }
}
