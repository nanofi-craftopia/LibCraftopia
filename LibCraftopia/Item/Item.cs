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
        public ItemData Inner { get; }

        public int Id
        {
            get => Inner.Id; set
            {
                AccessTools.FieldRefAccess<ItemData, int>(Inner, "id") = value;
                Inner.pKey = value;
            }
        }

        public int DevPriority { get => Inner.DevPriority; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "devPriority") = value; }
        public int DebugStatus { get => Inner.DebugStatus; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "status") = value; }
        public Sprite Icon { get => Inner.ItemImage_Icon; set => Inner.Set(value); }
        public int MaxStack { get => Inner.MaxStack; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "maxStack") = value; }
        public int Price { get => Inner.Price; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "price") = value; }
        public float ProbInTreasureBox { get => Inner.ProbInTreasureBox; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "probInTreasureBox") = value; }
        public int Rarity { get => Inner.Rarity; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "rarity") = value; }
        public int DisplayRarity { get => Inner.DisplayRarity; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "displayRarity") = value; }
        public ItemRarity DisplayRarityType { get => Inner.DisplayRarityType; set => DisplayRarity = (int)value; }
        public int CategoryId { get => Inner.CategoryId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "categoryId") = value; }
        public ItemCategory Category { get => Inner.ItemCategory; set => CategoryId = (int)value; }
        public int FamilyId { get => Inner.FamilyId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "familyId") = value; }
        public int InventryTabId { get => Inner.InventoryTabId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "inventoryTabId") = value; }
        public ItemType ItemType { get => Inner.ItemType; set => InventryTabId = (int)value; }
        public int WorkbenchId { get => Inner.WorkBenchId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "workbenchId") = value; }
        public int WorkbenchLevel { get => Inner.WorkBenchLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "workbenchLv") = value; }
        public int PlayerCraftCount { get => Inner.PlayerCraftCount; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "playerCraftCount") = value; }
        public float CraftTimeCost { get => Inner.CarftTimeCost; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "carftTimeCost") = value; }
        public ref int MaterialAId { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialA_Id"); }
        public ref int MaterialBId { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialB_Id"); }
        public ref int MaterialCId { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialC_Id"); }
        public ref int MaterialDId { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialD_Id"); }
        public ref int MaterialEId { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialE_Id"); }
        public ref int MaterialACount { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialA_count"); }
        public ref int MaterialBCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialB_count"); }
        public ref int MaterialCCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialC_count"); }
        public ref int MaterialDCount { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialD_count"); }
        public ref int MaterialECount { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "materialE_count"); }

        public float CooldownTime { get => Inner.CooldownTime; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "cooldownTime") = value; }
        public float EffectiveTime { get => Inner.EffectiveTime; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "effectiveTime") = value; }
        public float RestoreHealth { get => Inner.RestoreHealth; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreHealth") = value; }
        public float RestoreMana { get => Inner.RestoreMana; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreMana") = value; }
        public float RestoreSatiety { get => Inner.RestoreSatiety; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreSatiety") = value; }
        public float AddStamina { get => Inner.AddStamina; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "addStamina") = value; }

        public int ATK { get => Inner.ATK; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "atk") = value; }
        public ref int ATKIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "atkIncreasePerLv"); }
        public int DEF { get => Inner.DEF; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "def") = value; }
        public ref int DEFIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "defIncreasePerLv"); }
        public int MATK { get => Inner.MATK; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "matk") = value; }
        public ref int MATKIncreasePerLevel { get => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "matkIncreasePerLv"); }
        public int MotionSpeed { get => Inner.MotionSpeed; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "motionSpeed") = value; }
        public float DurabilityValue { get => Inner.DurabilityValue; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "durabilityValue") = value; }
        public int RegistFireLevel { get => Inner.RegistFireLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "registFireLv") = value; }
        public int RegistColdLevel { get => Inner.RegistColdLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "registColdLv") = value; }
        public int RegistPoisonLevel { get => Inner.RegistPoisonLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "registPoisonLv") = value; }
        public int PassiveSkillId { get => Inner.EquipmentPassiveSkillId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillId") = value; }
        public int PassiveSkillLevel { get => Inner.EquipmentPassiveSkillLevel; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillLevel") = value; }
        public int ActiveSkillId { get => Inner.EquipmentActiveSkillId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentActiveSkillId") = value; }
        public int ActiveSkillLevel { get => Inner.EquipmentActiveSkillLevel; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentActiveSkillLevel") = value; }
        public float ExtraValueA { get => Inner.ExtraValue_A; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "extraValue_A") = value; }
        public float ExtraValueB { get => Inner.ExtraValue_B; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "extraValue_B") = value; }
        public ItemHandlerSO Handler { get => Inner.Handler; set => AccessTools.FieldRefAccess<ItemData, ItemHandlerSO>(Inner, "handler") = value; }
        public OcItemPrefab ItemPrefab { get => Inner.ItemPrefab; set => AccessTools.FieldRefAccess<ItemData, OcItemPrefab>(Inner, "itemPrefab") = value; }
        public OcPlActiveSkillType ActiveSkillType { get => Inner.ActiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlActiveSkillType>(Inner, "activeSkillType") = value; }
        public OcPlPassiveSkillType PassiveSkillType { get => Inner.PassiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlPassiveSkillType>(Inner, "passiveSkillType") = value; }

        public bool IsEnabled { get => Inner.IsEnabled; }
        public bool IsAppearIngame { get => Inner.IsAppearIngame; }

        public Item()
        {
            Inner = ScriptableObject.CreateInstance<ItemData>();
            DebugStatus = 1;
            MaxStack = 99;
            Price = 1;
            Rarity = 1;
            PlayerCraftCount = 1;
            CraftTimeCost = 3;
        }
        public Item(ItemData itemData)
        {
            Inner = itemData;
        }

        public static implicit operator ItemData(Item item)
        {
            return item.Inner;
        }
        public static implicit operator Item(ItemData item)
        {
            return new Item(item);
        }
    }
}
