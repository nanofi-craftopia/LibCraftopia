using HarmonyLib;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemList
    {
        private ItemManager manager;
        internal ItemList(ItemManager manager)
        {
            this.manager = manager;
        }

        public SoItemDataList Original => AccessTools.FieldRefAccess<OcItemDataMng, SoItemDataList>(manager.Original, "SoItemDataList");

        public int MaxRank => AccessTools.StaticFieldRefAccess<SoItemDataList, int>("MaxRank");

        public ref ItemData[] All => ref AccessTools.FieldRefAccess<SoDataList<SoItemDataList, ItemData>, ItemData[]>(Original, "all");

        public Sprite EquipIcon => AccessTools.FieldRefAccess<SoItemDataList, Sprite>(Original, "equipIcon");
        public Sprite ConsumableIcon => AccessTools.FieldRefAccess<SoItemDataList, Sprite>(Original, "consumableIcon");
        public Sprite MaterialIcon => AccessTools.FieldRefAccess<SoItemDataList, Sprite>(Original, "materialIcon");
        public Sprite MagicItemIcon => AccessTools.FieldRefAccess<SoItemDataList, Sprite>(Original, "magicItemIcon");
        public Sprite BuildingIcon => AccessTools.FieldRefAccess<SoItemDataList, Sprite>(Original, "buildingIcon");
        public ItemHandlerSO DefaultHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "defaultHandler");
        public ItemHandlerSO EquipHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "equipHandler");
        public ItemHandlerSO PotionHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "potionHandler");
        public ItemHandlerSO SkillHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "skillHandler");
        public ItemHandlerSO BreedSeedHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "breedSeedHandler");
        public ItemHandlerSO UseActHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "useActHandler");
        public ItemHandlerSO RandomItemHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "randomItemHandler");
        public ItemHandlerSO BlueprintHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "blueprintHandler");
        public ItemHandlerSO MilionConstractDocumentUnconstractedHandler => AccessTools.FieldRefAccess<SoItemDataList, ItemHandlerSO>(Original, "milionConstractDocument_UnconstractedHandler");

        public ref float[] RarityChestProbSums => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarityChestProbSums");
        public ref float[] Rarity1ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity1ChestProbs");
        public ref float[] Rarity2ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity2ChestProbs");
        public ref float[] Rarity3ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity3ChestProbs");
        public ref float[] Rarity4ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity4ChestProbs");
        public ref float[] Rarity5ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity5ChestProbs");
        public ref float[] Rarity6ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity6ChestProbs");
        public ref float[] Rarity7ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity7ChestProbs");
        public ref float[] Rarity8ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity8ChestProbs");
        public ref float[] Rarity9ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity9ChestProbs");
        public ref float[] Rarity10ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity10ChestProbs");
        public ref float[] Rarity11ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity11ChestProbs");
        public ref float[] Rarity12ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity12ChestProbs");
        public ref float[] Rarity13ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity13ChestProbs");
        public ref float[] Rarity14ChestProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "rarity14ChestProbs");

        public ref float[] RefRarityChestProbs(int rank)
        {
            switch (rank)
            {
                case 1: return ref Rarity1ChestProbs;
                case 2: return ref Rarity2ChestProbs;
                case 3: return ref Rarity3ChestProbs;
                case 4: return ref Rarity4ChestProbs;
                case 5: return ref Rarity5ChestProbs;
                case 6: return ref Rarity6ChestProbs;
                case 7: return ref Rarity7ChestProbs;
                case 8: return ref Rarity8ChestProbs;
                case 9: return ref Rarity9ChestProbs;
                case 10: return ref Rarity10ChestProbs;
                case 11: return ref Rarity11ChestProbs;
                case 12: return ref Rarity12ChestProbs;
                case 13: return ref Rarity13ChestProbs;
                case 14: return ref Rarity14ChestProbs;
                default: throw new ArgumentOutOfRangeException(nameof(rank));
            }
        }

        public ref float[] WeaponGachaProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "weaponGachaProbs");
        public ref float WeaponGachaProbSum => ref AccessTools.FieldRefAccess<SoItemDataList, float>(Original, "weaponGachaProbSum");
        public ref float[] MaterialGachaProbs => ref AccessTools.FieldRefAccess<SoItemDataList, float[]>(Original, "materialGachaProbs");
        public ref float MaterialGachaProbSum => ref AccessTools.FieldRefAccess<SoItemDataList, float>(Original, "materialGachaProbSum");
    }
}
