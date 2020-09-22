using HarmonyLib;
using JetBrains.Annotations;
using LibCraftopia.Container;
using LibCraftopia.Registry;
using MapMagic;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class Item : IRegistryEntry
    {
        private readonly ItemData inner;
        public ItemData Inner { get => inner; }

        public int Id
        {
            get => inner.Id; set
            {
                AccessTools.FieldRefAccess<ItemData, int>(inner, "id") = value;
                AccessTools.FieldRefAccess<ItemData, int>(inner, "pKey") = value;

            }
        }

        public int DevPriority { get => inner.DevPriority; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "devPriority") = value; }
        public int DebugStatus { get => inner.DebugStatus; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "status") = value; }
        public Sprite Icon { get => inner.ItemImage_Icon; set => inner.Set(value); }
        public int MaxStack { get => inner.MaxStack; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "maxStack") = value; }
        public int Price { get => inner.Price; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "price") = value; }
        public float ProbInTreasureBox { get => inner.ProbInTreasureBox; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "probInTreasureBox") = value; }
        public int Rarity { get => inner.Rarity; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "rarity") = value; }
        public int DisplayRarity { get => inner.DisplayRarity; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "displayRarity") = value; }
        public ItemRarity DisplayRarityType { get => inner.DisplayRarityType; set => DisplayRarity = (int)value; }
        public int CategoryId { get => inner.CategoryId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "categoryId") = value; }
        public ItemCategory Category { get => inner.ItemCategory; set => CategoryId = (int)value; }
        public int FamilyId { get => inner.FamilyId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "familyId") = value; }
        public int InventryTabId { get => inner.InventoryTabId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "inventoryTabId") = value; }
        public ItemType ItemType { get => inner.ItemType; set => InventryTabId = (int)value; }
        public int WorkbenchId { get => inner.WorkBenchId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "workbenchId") = value; }
        public int WorkbenchLevel { get => inner.WorkBenchLv; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "workbenchLv") = value; }
        public int PlayerCraftCount { get => inner.PlayerCraftCount; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "playerCraftCount") = value; }
        public float CraftTimeCost { get => inner.CarftTimeCost; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "carftTimeCost") = value; }
        public ref int MaterialAId { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialA_Id"); }
        public ref int MaterialBId { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialB_Id"); }
        public ref int MaterialCId { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialC_Id"); }
        public ref int MaterialDId { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialD_Id"); }
        public ref int MaterialEId { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialE_Id"); }
        public ref int MaterialACount { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialA_count"); }
        public ref int MaterialBCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialB_count"); }
        public ref int MaterialCCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialC_count"); }
        public ref int MaterialDCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialD_count"); }
        public ref int MaterialECount { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "materialE_count"); }

        public float CooldownTime { get => inner.CooldownTime; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "cooldownTime") = value; }
        public float EffectiveTime { get => inner.EffectiveTime; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "effectiveTime") = value; }
        public float RestoreHealth { get => inner.RestoreHealth; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "restoreHealth") = value; }
        public float RestoreMana { get => inner.RestoreMana; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "restoreMana") = value; }
        public float RestoreSatiety { get => inner.RestoreSatiety; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "restoreSatiety") = value; }
        public float AddStamina { get => inner.AddStamina; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "addStamina") = value; }

        public int ATK { get => inner.ATK; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "atk") = value; }
        public ref int ATKIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "atkIncreasePerLv"); }
        public int DEF { get => inner.DEF; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "def") = value; }
        public ref int DEFIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "defIncreasePerLv"); }
        public int MATK { get => inner.MATK; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "matk") = value; }
        public ref int MATKIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(inner, "matkIncreasePerLv"); }
        public int MotionSpeed { get => inner.MotionSpeed; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "motionSpeed") = value; }
        public float DurabilityValue { get => inner.DurabilityValue; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "durabilityValue") = value; }
        public int RegistFireLevel { get => inner.RegistFireLv; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "registFireLv") = value; }
        public int RegistColdLevel { get => inner.RegistColdLv; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "registColdLv") = value; }
        public int RegistPoisonLevel { get => inner.RegistPoisonLv; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "registPoisonLv") = value; }
        public int PassiveSkillId { get => inner.EquipmentPassiveSkillId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "equipmentPassiveSkillId") = value; }
        public int PassiveSkillLevel { get => inner.EquipmentPassiveSkillLevel; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "equipmentPassiveSkillLevel") = value; }
        public int ActiveSkillId { get => inner.EquipmentActiveSkillId; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "equipmentActiveSkillId") = value; }
        public int ActiveSkillLevel { get => inner.EquipmentActiveSkillLevel; set => AccessTools.FieldRefAccess<ItemData, int>(inner, "equipmentActiveSkillLevel") = value; }
        public float ExtraValueA { get => inner.ExtraValue_A; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "extraValue_A") = value; }
        public float ExtraValueB { get => inner.ExtraValue_B; set => AccessTools.FieldRefAccess<ItemData, float>(inner, "extraValue_B") = value; }
        public ItemHandlerSO Handler { get => inner.Handler; set => AccessTools.FieldRefAccess<ItemData, ItemHandlerSO>(inner, "handler") = value; }
        public OcItemPrefab ItemPrefab { get => inner.ItemPrefab; set => AccessTools.FieldRefAccess<ItemData, OcItemPrefab>(inner, "itemPrefab") = value; }
        public OcPlActiveSkillType ActiveSkillType { get => inner.ActiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlActiveSkillType>(inner, "activeSkillType") = value; }
        public OcPlPassiveSkillType PassiveSkillType { get => inner.PassiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlPassiveSkillType>(inner, "passiveSkillType") = value; }

        public bool IsEnabled { get => inner.IsEnabled; }
        public bool IsAppearIngame { get => inner.IsAppearIngame; }

        public Item()
        {
            inner = ScriptableObject.CreateInstance<ItemData>();
            DebugStatus = 1;
            MaxStack = 99;
            Price = 1;
            Rarity = 1;
            PlayerCraftCount = 1;
            CraftTimeCost = 3;
        }
        public Item(ItemData itemData)
        {
            inner = itemData;
        }

        public static implicit operator ItemData(Item item)
        {
            return item.inner;
        }
        public static implicit operator Item(ItemData item)
        {
            return new Item(item);
        }
    }
}
