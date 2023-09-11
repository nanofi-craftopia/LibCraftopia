using HarmonyLib;
using JetBrains.Annotations;
using LibCraftopia.Container;
using LibCraftopia.Localization;
using LibCraftopia.Registry;
using Oc;
using Oc.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        public int Status { get => Inner.Status; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "status") = value; }
        public bool IsOldData => Inner.IsOldData;
        public bool IsEnabled => Inner.IsEnabled;
        public bool IsAppearIngame => Inner.IsAppearIngame;
        public bool IsAppearIngameWithoutAdmin => Inner.IsAppearIngameWithoutAdmin;
        public Sprite Icon { get => Inner.ItemImage_Icon; set => Inner.Set(value); }
        public int MaxStack { get => Inner.MaxStack; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "maxStack") = value; }
        public string DisplayName => Inner.DisplayName;
        public string Description => Inner.Description;
        public int Price { get => Inner.Price; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "price") = value; }
        public float ProbInTreasureBox { get => Inner.ProbInTreasureBox; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "probInTreasureBox") = value; }
        public float ProbInGacha { get => Inner.ProbInGacha; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "probInGacha") = value; }
        public int Rank { get => Inner.Rank; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "rank") = value; }
        public int Rarity { get => Inner.Rarity; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "rarity") = value; }
        public ItemRarity DisplayRarityType => Inner.DisplayRarityType;
        public ItemType ItemType { get => Inner.ItemType; set => InventryTabId = (int)value; }
        public int CategoryId { get => Inner.CategoryId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "categoryId") = value; }
        public ItemCategory Category { get => Inner.ItemCategory; set => CategoryId = (int)value; }
        public int FamilyId { get => Inner.FamilyId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "familyId") = value; }
        public int InventryTabId { get => Inner.InventoryTabId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "inventoryTabId") = value; }
        public int WorkbenchId { get => Inner.WorkBenchId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "workbenchId") = value; }
        public int WorkbenchLv { get => Inner.WorkBenchLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "workbenchLv") = value; }
        public string DisplayWorkBenchLv => Inner.DisplayWorkBenchLv;
        public int CountPerCraft { get => Inner.CountPerCraft; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "playerCraftCount") = value; }
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
        public float RestoreHealthRate { get => Inner.RestoreHealthRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreHealthRate") = value; }
        public float RestoreMana { get => Inner.RestoreMana; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreMana") = value; }
        public float RestoreManaRate { get => Inner.RestoreManaRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreManaRate") = value; }
        public float RestoreSatiety { get => Inner.RestoreSatiety; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "restoreSatiety") = value; }
        public float AddStamina { get => Inner.AddStamina; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "addStamina") = value; }
        public float BuffMaxHp { get => Inner.BuffMaxHp; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffMaxHealth") = value; }
        public float BuffMaxMp { get => Inner.BuffMaxMp; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffMaxMana") = value; }
        public float BuffAtk { get => Inner.BuffAtk; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffAtk") = value; }
        public float BuffMatk { get => Inner.BuffMatk; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffMatk") = value; }
        public float BuffDef { get => Inner.BuffDef; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffDef") = value; }
        public float BuffResistFireLv { get => Inner.BuffResistFireLv; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffResistFireLv") = value; }
        public float BuffResistColdLv { get => Inner.BuffResistColdLv; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffResistColdLv") = value; }
        public float BuffResistPoisonLv { get => Inner.BuffResistPoisonLv; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffResistPoisonLv") = value; }
        public float BuffResistHellFireLv { get => Inner.BuffResistHellFireLv; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffResistHellFireLv") = value; }
        public float BuffAtkBoss { get => Inner.BuffAtkBoss; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffAtkBoss") = value; }
        public float BuffMatkBoss { get => Inner.BuffMatkBoss; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffMatkBoss") = value; }
        public float BuffDefBoss { get => Inner.BuffDefBoss; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffDefBoss") = value; }
        public float BuffFinalDamageRate { get => Inner.BuffFinalDamageRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate") = value; }
        public float BuffFinalDamageRateBow { get => Inner.BuffFinalDamageRate_Bow; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Bow") = value; }
        public float BuffFinalDamageRateSword { get => Inner.BuffFinalDamageRate_Sword; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Sword") = value; }
        public float BuffFinalDamageRateDual { get => Inner.BuffFinalDamageRate_Dual; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Dual") = value; }
        public float BuffFinalDamageRateLance { get => Inner.BuffFinalDamageRate_Lance; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Lance") = value; }
        public float BuffFinalDamageRateTwoHandSword { get => Inner.BuffFinalDamageRate_TwoHandSword; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_TwoHandSword") = value; }
        public float BuffFinalDamageRateNoWeapon { get => Inner.BuffFinalDamageRate_NoWeapon; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_NoWeapon") = value; }
        public float BuffFinalDamageRateKatana { get => Inner.BuffFinalDamageRate_Katana; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Katana") = value; }
        public float BuffFinalDamageRateGun { get => Inner.BuffFinalDamageRate_Gun; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_Gun") = value; }
        public float BuffFinalDamageRateMagicStaff { get => Inner.BuffFinalDamageRate_MagicStaff; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffFinalDamageRate_MagicStaff") = value; }
        public float BuffIncreaseAtkByDefRate { get => Inner.BuffIncreaseAtkByDefRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseAtkByDefRate") = value; }
        public float BuffIncreaseMAtkByDefRate { get => Inner.BuffIncreaseMAtkByDefRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseMAtkByDefRate") = value; }
        public float BuffIncreaseAtkByHpRate { get => Inner.BuffIncreaseAtkByHpRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseAtkByHpRate") = value; }
        public float BuffIncreaseMAtkByHpRate { get => Inner.BuffIncreaseMAtkByHpRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseMAtkByHpRate") = value; }
        public float BuffIncreaseAtkByMpRate { get => Inner.BuffIncreaseAtkByMpRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseAtkByMpRate") = value; }
        public float BuffIncreaseMAtkByMpRate { get => Inner.BuffIncreaseMAtkByMpRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "buffIncreaseMAtkByMpRate") = value; }

        public ref int Atk => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "atk");
        public int GetAtk(int level) { return Inner.GetAtk(level); }
        public ref int Def => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "def");
        public int GetDef(int level) { return Inner.GetDef(level); }
        public int GetDefUI(int level) { return Inner.GetDef_UI(level); }
        public ref int MAtk => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "matk");
        public int GetMAtk(int level) { return Inner.GetMAtk(level); }
        public bool IsRefinable() => Inner.IsRefinable();
        public float MotionSpeed { get => Inner.MotionSpeed; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "motionSpeed") = value; }
        public float MovementSpeed { get => Inner.MovementSpeed; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "movementSpeed") = value; }
        public float JumpPow { get => Inner.JumpPow; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "jumpPow") = value; }
        public float DurabilityValue { get => Inner.DurabilityValue; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "durabilityValue") = value; }
        public float ProficientMaxValue { get => Inner.ProficientMaxValue; set => DurabilityValue = value; }
        public int ResistFireLv { get => Inner.ResistFireLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "resistFireLv") = value; }
        public int ResistColdLv { get => Inner.ResistColdLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "resistColdLv") = value; }
        public int ResistPoisonLv { get => Inner.ResistPoisonLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "resistPoisonLv") = value; }
        public int ResistHellFireLv { get => Inner.ResistHellFireLv; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "resistHellFireLv") = value; }
        public float ResistElementFireRate { get => Inner.ResistElementFireRate; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "resistElementFireRate") = value; }
        public int HellFireValue { get => Inner.HellFireValue; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "hellFireValue") = value; }
        public int PoisonValue { get => Inner.PoisonValue; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "poisonValue") = value; }
        public int FrozenVal { get => Inner.FrozenVal; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "frozenVal") = value; }
        public int ChillVal { get => Inner.ChillVal; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "chillVal") = value; }
        public int ElectroVal { get => Inner.ElectroVal; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "electroVal") = value; }
        public int ElementFireDamage { get => Inner.ElementFireDamage; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "elementFireValue") = value; }
        public int ElementIceDamage { get => Inner.ElementIceDamage; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "elementIceValue") = value; }

        public int EnchantID { get => Inner.EnchantID; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "enchantID") = value; }
        public ref int LootEnchantId00 => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "lootEnchantId_00");
        public ref int LootEnchantId01 => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "lootEnchantId_01");
        public ref int LootEnchantId02 => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "lootEnchantId_02");
        public ref int LootEnchantId03 => ref AccessTools.FieldRefAccess<ItemData, int>(Inner, "lootEnchantId_03");

        public int EquipmentActiveSkillId { get => Inner.EquipmentActiveSkillId; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentActiveSkillId") = value; }
        public int EquipmentActiveSkillLevel { get => Inner.EquipmentActiveSkillLevel; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentActiveSkillLevel") = value; }
        public int EquipmentPassiveSkillId1 { get => Inner.EquipmentPassiveSkillId1; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillId1") = value; }
        public int EquipmentPassiveSkillLevel1 { get => Inner.EquipmentPassiveSkillLevel1; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillLevel1") = value; }
        public int EquipmentPassiveSkillId2 { get => Inner.EquipmentPassiveSkillId2; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillId2") = value; }
        public int EquipmentPassiveSkillLevel2 { get => Inner.EquipmentPassiveSkillLevel2; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillLevel2") = value; }
        public int EquipmentPassiveSkillId3 { get => Inner.EquipmentPassiveSkillId3; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillId3") = value; }
        public int EquipmentPassiveSkillLevel3 { get => Inner.EquipmentPassiveSkillLevel3; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillLevel3") = value; }
        public int EquipmentPassiveSkillId4 { get => Inner.EquipmentPassiveSkillId4; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillId4") = value; }
        public int EquipmentPassiveSkillLevel4 { get => Inner.EquipmentPassiveSkillLevel4; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "equipmentPassiveSkillLevel4") = value; }

        public float ExtraValueA { get => Inner.ExtraValue_A; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "extraValue_A") = value; }
        public float ExtraValueB { get => Inner.ExtraValue_B; set => AccessTools.FieldRefAccess<ItemData, float>(Inner, "extraValue_B") = value; }

        public ItemHandlerSO Handler { get => Inner.Handler; set => AccessTools.FieldRefAccess<ItemData, ItemHandlerSO>(Inner, "handler") = value; }
        public OcItemPrefab ItemPrefab => Inner.ItemPrefab;
        public Sprite BuildingDecoIcon { get => Inner.BuildingDecoIcon; set => AccessTools.FieldRefAccess<ItemData, Sprite>(Inner, "buildingDecoIcon") = value; }
        public OcPlActiveSkillType ActiveSkillType { get => Inner.ActiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlActiveSkillType>(Inner, "activeSkillType") = value; }
        public OcPlPassiveSkillType PassiveSkillType { get => Inner.PassiveSkillType; set => AccessTools.FieldRefAccess<ItemData, OcPlPassiveSkillType>(Inner, "passiveSkillType") = value; }
        public bool CanUse { get => Inner.CantUse; set => AccessTools.FieldRefAccess<ItemData, bool>(Inner, "cantUse") = value; }
        public int DeliveryCategory { get => Inner.DeliveryCategory; set => AccessTools.FieldRefAccess<ItemData, int>(Inner, "deliveryCategory") = value; }
        public GameObject RawItemModel => Inner.RawItemModel;
        public OcPlEquip EquipPrefab => Inner.EquipPrefab;
        public bool IsEquipable => Inner.IsEquipable;
        public bool IsCraftable => Inner.IsCraftable;
        public bool IsCookedFood => Inner.IsCookedFood;

        public byte RefineMaxLv { get => Inner.Refine_MaxLv; set => AccessTools.FieldRefAccess<ItemData, byte>(Inner, "_Refine_MaxLv") = value; }
        public string CreateLevelText(int level) => Inner.CreateLevelText(level);

        public ItemPrefabExist ExistPrefab { get => Inner.ExistPrefab; set => AccessTools.FieldRefAccess<ItemData, ItemPrefabExist>(Inner, "existPrefab") = value; }
        public OcEquipSlot EquipSlot { get => Inner.EquipSlot; set => AccessTools.FieldRefAccess<ItemData, OcEquipSlot>(Inner, "equipSlot") = value; }
        public OcWpCategory WpCategory { get => Inner.WpCategory; set => AccessTools.FieldRefAccess<ItemData, OcWpCategory>(Inner, "wpCategory") = value; }
        public SoItemLotteryTeble SoItemLotteryTeble { get => Inner.SoItemLotteryTeble; set => AccessTools.FieldRefAccess<ItemData, SoItemLotteryTeble>(Inner, "_soItemLotteryTeble") = value; }
        public SoGunParam SoGunParam { get => Inner.SoGunParam; set => AccessTools.FieldRefAccess<ItemData, SoGunParam>(Inner, "_SoGunParam") = value; }
        public SoGunAssistEquipParam SoGunAssistEquipParam { get => Inner.SoGunAssistEquipParam; set => AccessTools.FieldRefAccess<ItemData, SoGunAssistEquipParam>(Inner, "_SoGunAssistEquipParam") = value; }

        public class EnchantInfluencesModifier
        {
            private static readonly FieldInfo enchantInfluencesField = AccessTools.Field(typeof(ItemData), "enchantInfluences");
            private ItemData inner;
            public List<KeyValuePair<int, Sprite>> Icons { get; private set; } = new List<KeyValuePair<int, Sprite>>();
            public EnchantInfluencesModifier(ItemData inner)
            {
                this.inner = inner;
            }

            public void Retrive()
            {
                Icons.Clear();
                var arr = enchantInfluencesField.GetValue(inner) as IEnumerable;
                var elemType = arr.GetType().GetElementType();
                var fieldEnchantId = elemType.GetField("enchantId");
                var fieldInfluence = elemType.GetField("influence");
                var fieldChangedIcon = fieldInfluence.FieldType.GetField("changedIcon");
                foreach (var val in arr)
                {
                    var enchantId = (int)fieldEnchantId.GetValue(val);
                    var influence = fieldInfluence.GetValue(val);
                    var changedIcon = (Sprite)fieldChangedIcon.GetValue(influence);
                    Icons.Add(new KeyValuePair<int, Sprite>(enchantId, changedIcon));
                }
            }
            public void Apply()
            {
                var elemType = enchantInfluencesField.FieldType.GetElementType();
                var fieldEnchantId = elemType.GetField("enchantId");
                var fieldInfluence = elemType.GetField("influence");
                var fieldChangedIcon = fieldInfluence.FieldType.GetField("changedIcon");
                var arr = Array.CreateInstance(elemType, Icons.Count);
                for (int i = 0; i < Icons.Count; i++)
                {
                    var enchantIcon = Icons[i];
                    var elem = Activator.CreateInstance(elemType);
                    fieldEnchantId.SetValue(elem, enchantIcon.Key);
                    var influence = Activator.CreateInstance(fieldInfluence.FieldType);
                    fieldChangedIcon.SetValue(influence, enchantIcon.Value);
                    fieldInfluence.SetValue(elem, influence);
                    arr.SetValue(elem, i);
                }
                enchantInfluencesField.SetValue(inner, arr);
            }
        }
        private EnchantInfluencesModifier enchantInfluences;
        public EnchantInfluencesModifier EnchantInfluences
        {
            get
            {
                enchantInfluences ??= new EnchantInfluencesModifier(this.Inner);
                return enchantInfluences;
            }
        }

        private float[] probsInTreasureBox;
        /// <summary>
        /// Probabilities that items obtained from treasure chests have this enchantment. The length of this property corresponds to the number of rarity types the treasure chest can have, which can be obtained from `EnchantHelper.Inst.MaxRarity`. 
        /// </summary>
        public float[] ProbsInTreasureBox
        {
            get => probsInTreasureBox;
            set
            {
                if (value != null && value.Length != ItemManager.Inst.ItemList.MaxRank)
                    throw new ArgumentException($"ProbInTreasureBox must be a float arrray with length {ItemManager.Inst.ItemList.MaxRank}, but the assigned value's length was {value.Length}.", "value");
                probsInTreasureBox = value;
            }
        }

        public Item()
        {
            Inner = ScriptableObject.CreateInstance<ItemData>();
            Status = 1;
            MaxStack = 100;
            Price = 1;
            Rarity = 1;
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
