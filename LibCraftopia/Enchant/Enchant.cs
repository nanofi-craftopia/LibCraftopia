using HarmonyLib;
using LibCraftopia.Localization;
using LibCraftopia.Registry;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Enchant
{
    public enum EnchantStatusModify
    {
        Atk,
        AtkRate,
        Def,
        DefRate,
        MAtk,
        MAtkRate,
        MaxHp,
        MaxHpRate,
        MaxMp,
        MaxMpRate,
        MaxSp,
        MaxSpRate,
        MaxSt,
        MaxStRate,
        CriticalDmgRate_Physical,
        CriticalDmgRate_Magical,
        SpConsumeRate,
        StConsumeRate,
        ManaConsumeRate,
        StRegenerateRate,
        ManaRegenerateRate,
        SkillCoolDownRate,
        ItemCoolDownRate,
        CriticalProb_Physical,
        CriticalProb_Magical,
        ItemDropProb,
        PoisonProb,
        FireProb,
        MovementSpeedRate,
        MovementSpeedRate_Air,
        MotionSpeedRate,
        JumpSpeedRate,
        AtkUndead,
        DefUndead,
        AtkIce,
        DefIce,
        AtkFire,
        DefFire,
        AtkBoss,
        MAtkBoss,
        DefBoss,
        AtkAnimal,
        DamageCut,
        JumpCount,
        IncreaseAtkByDefRate,
        IncreaseMAtkByDefRate,
        IncreaseAtkByHpRate,
        IncreaseMAtkByHpRate,
        IncreaseAtkByMpRate,
        IncreaseMAtkByMpRate,
        HealthSkillHealRate,
        FinalDamageRate,
        FinalPhysicsDamageRate,
        FinalMagicDamageRate,
        FinalArrowDamageRate,
        FinalUnarmedDamgeRate,
        FinalSkillDamageRate,
        FinalMeleeSkillDamageRate,
        PetAtkRate,
        PetDefRate,
        PetCriticalRate,
        PetCriticalDamageRate,
        PetSpeedRate,
    }
    public enum EnchantLimitedCategory
    {
        Normal = 0,
        Equipment = 10000,
        Item = 30000
    }
    public class Enchant : IRegistryEntry
    {
        public SoEnchantment Inner { get; }

        public int Id { get => Inner.ID; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "id") = value; }
        public int Status { get => Inner.Status; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "status") = value; }
        public bool IsEnabled => Inner.IsEnabled;
        public bool IsOldData => Inner.IsOldData;
        public int IsTreasureDropped { get => Inner.IsTreasureDropped; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "isTreasureDropped") = value; }
        public EnchantRarity Rarity { get => Inner.Rarity; set => AccessTools.FieldRefAccess<SoEnchantment, EnchantRarity>(Inner, "rarity") = value; }
        public int LimitedCategoryId { get => Inner.LimitedCategoryId; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "limitedCategoryId") = value; }
        public string DisplayName => Inner.DisplayName;
        public bool OnlyShield { get => Inner.Only_Shield; set => AccessTools.FieldRefAccess<SoEnchantment, bool>(Inner, "only_Shield") = value; }
        public bool OnlyAccessory { get => Inner.Only_Accessory; set => AccessTools.FieldRefAccess<SoEnchantment, bool>(Inner, "only_Accessory") = value; }
        public bool OnlyWeaponSlot { get => Inner.Only_WeaponSlot; set => AccessTools.FieldRefAccess<SoEnchantment, bool>(Inner, "only_WeaponSlot") = value; }
        public bool CantDupe { get => Inner.Cant_Dupe; set => AccessTools.FieldRefAccess<SoEnchantment, bool>(Inner, "cant_Dupe") = value; }

        public float PriceModify { get => Inner.PriceModify; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "modify_PriceRate") = value; }
        public float RestoreEffectRate { get => Inner.GetRestoreEffectRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreEffectRate") = value; }
        public float RestoreHealth { get => Inner.GetRestoreHealth(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreHealth") = value; }
        public float RestoreHealthRate { get => Inner.GetRestoreHealthRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreHealthRate;") = value; }
        public float RestoreMana { get => Inner.GetRestoreMana(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreMana") = value; }
        public float RestoreManaRate { get => Inner.GetRestoreManaRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreManaRate") = value; }
        public float RestoreSatiety { get => Inner.GetRestoreSatiety(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreSatiety") = value; }
        public float RestoreSatietyRate { get => Inner.GetRestoreSatietyRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_RestoreSatietyRate") = value; }
        public float AtkBuffRate { get => Inner.GetAtkBuffRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_AtkBuffRate") = value; }
        public float MatkBuffRate { get => Inner.GetMatkBuffRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_MatkBuffRate") = value; }
        public float DefBuffRate { get => Inner.GetDefBuffRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_DefBuffRate") = value; }
        public float AllEffectRate { get => Inner.GetAllEffectRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_AllEffectRate") = value; }
        public float CooldownBuff { get => Inner.GetCooldownBuff(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_CooldownBuff") = value; }
        public float WithoutConsumptionRate { get => Inner.GetWithoutConsumptionRate(); set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "use_WithoutConsumptionRate") = value; }


        public int EquipmentPassiveSkillId { get => Inner.EquipmentPassiveSkillId; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "equipmentPassiveSkillId") = value; }
        public int EquipmentPassiveSkillLevel { get => Inner.EquipmentPassiveSkillLevel; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "equipmentPassiveSkillLevel") = value; }


        public int ProductEnchant { get => Inner.ProductEnchant; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "building_ProductEnchant") = value; }
        public float ProduceSpeedModify { get => Inner.ProduceSpeedModify; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_ProduceSpeedRate") = value; }
        public float NoUseCraftMaterialRate { get => Inner.NoUseCraftMaterialRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_NoUseCraftMaterialRate") = value; }
        public float NoUseRefiningMaterialRate { get => Inner.NoUseRefiningMaterialRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_NoUseRefiningMaterialRate") = value; }
        public float NoUseFeedRate { get => Inner.NoUseFeedRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_NoUseFeedRate") = value; }
        public float ProduceDoubleRate { get => Inner.ProduceDoubleRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_ProduceDoubleRate") = value; }
        public float HarvestDoubleRate { get => Inner.HarvestDoubleRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_HarvestDoubleRate") = value; }
        public float BuildingMaxHealthRate { get => Inner.BuildingMaxHealthRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_MaxHealthRate") = value; }
        public float BuildingFireDamageRate { get => Inner.BuildingFireDamageRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_FireDamageRate") = value; }
        public float BuildingFireResistRate { get => Inner.BuildingFireResistRate; set => AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, "building_FireResistRate") = value; }
        public bool CantWash { get => Inner.CantWash; set => AccessTools.FieldRefAccess<SoEnchantment, bool>(Inner, "cantWash") = value; }


        public ref float Modify(EnchantStatusModify modify)
        {
            return ref AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, $"modify_{modify.ToString()}");
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
                if (value != null && value.Length != EnchantList.Inst.MaxRarity)
                    throw new ArgumentException($"ProbInTreasureBox must be a float arrray with length {EnchantList.Inst.MaxRarity}, but the assigned value's length was {value.Length}.", "value");
                probsInTreasureBox = value;
            }
        }

        public Enchant()
        {
            Inner = ScriptableObject.CreateInstance<BlankEnchantmentSource>();
            Status = 1;
        }
        public Enchant(SoEnchantment enchantment)
        {
            Inner = enchantment;
        }

        public static implicit operator SoEnchantment(Enchant enchant)
        {
            return enchant.Inner;
        }
        public static implicit operator Enchant(SoEnchantment enchantment)
        {
            return new Enchant(enchantment);
        }
    }
}
