using HarmonyLib;
using LibCraftopia.Helper;
using LibCraftopia.Registry;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Enchant
{
    public enum EnchantEffect
    {
        modify_Atk,
        modify_AtkRate,
        modify_Def,
        modify_DefRate,
        modify_MAtk,
        modify_MAtkRate,
        modify_MaxHp,
        modify_MaxHpRate,
        modify_MaxMp,
        modify_MaxMpRate,
        modify_MaxSp,
        modify_MaxSpRate,
        modify_MaxSt,
        modify_MaxStRate,
        modify_CriticalDmgRate,
        modify_SpConsumeRate,
        modify_StConsumeRate,
        modify_ManConsumeRate,
        modify_StRecoverRate,
        modify_ManaRecoverRate,
        modify_SkillCoolDownRate,
        modify_ItemCoolDownRate,
        modify_CriticalProb,
        modify_ItemDropProb,
        modify_PoisonProb,
        modify_FireProb,
        modify_MovementSpeedRate,
        modify_MotionSpeedRate,
        modify_JumpSpeedRate,
        modify_AtkUndead,
        modify_DefUndead,
        modify_AtkIce,
        modify_DefIce,
        modify_AtkFire,
        modify_DefFire,
        modify_AtkBoss,
        modify_DefBoss,
        modify_AtkAnimal,
        modify_DamageCut,
        modify_JumpCount,
        modify_PriceRate,
        use_RestoreHealth,
        use_RestoreMana,
        use_RestoreSatiety,
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
        public EnchantRarity Rarity { get => Inner.Rarity; set => AccessTools.FieldRefAccess<SoEnchantment, EnchantRarity>(Inner, "rarity") = value; }
        public int LimitedCategoryId { get => Inner.LimitedCategoryId; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "limitedCategoryId") = value; }
        public int PassiveSkillId { get => Inner.EquipmentPassiveSkillId; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "equipmentPassiveSkillId") = value; }
        public int PassiveSkillLevel { get => Inner.EquipmentPassiveSkillLevel; set => AccessTools.FieldRefAccess<SoEnchantment, int>(Inner, "equipmentPassiveSkillLevel") = value; }

        public ref float this[EnchantEffect effect]
        {
            get => ref AccessTools.FieldRefAccess<SoEnchantment, float>(Inner, effect.ToString());
        }

        private float[] probInTreasureBoxes;
        /// <summary>
        /// Probabilities that items obtained from treasure chests have this enchantment. The length of this property corresponds to the number of rarity types the treasure chest can have, which can be obtained from `EnchantHelper.Inst.MaxRarity`. 
        /// </summary>
        public float[] ProbInTreasureBox
        {
            get => probInTreasureBoxes;
            set
            {
                if (value != null && value.Length != EnchantHelper.Inst.MaxRarity)
                    throw new ArgumentException($"ProbInTreasureBox must be a float arrray with length {EnchantHelper.Inst.MaxRarity}, but the assigned value's length was {value.Length}.", "value");
                probInTreasureBoxes = value;
            }
        }
        /// <summary>
        /// Probability that the enemies' drop items have this enchantment. This affects all the enemies' drops.
        /// </summary>
        public float ProbInRandomDrop { get; set; }
        /// <summary>
        /// Probability that items obtained by attacking or destroying stone-like object, including ore, have this enchantment. This affects all drops from the stone objects and bed rock objects.
        /// </summary>
        public float ProbInStoneDrop { get; set; }
        /// <summary>
        /// Probability that items obtained by attacking or destroying tree object have this enchantment. This affects all drops from tree objects.
        /// </summary>
        public float ProbInTreeDrop { get; set; }
        /// <summary>
        /// Probability that a specific enemy whose id corresponding to the key gives a item with this enchantment. This affects only a specific enemy's drop.
        /// </summary>
        public Dictionary<int, float> ProbInEnemyDrop { get; } = new Dictionary<int, float>();

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
