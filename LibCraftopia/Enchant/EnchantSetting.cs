using System;
using System.Collections.Generic;
using System.Text;
using Oc;
using LibCraftopia.Helper;
using HarmonyLib;
using Oc.Item;
using UnityEngine;

namespace LibCraftopia.Enchant
{
    //SoEnchantment does not store drop-related information. 
    //Therefore, this class should not be used as a builder 
    //because it is inefficient if drop-related processing is not done in batches.
    public class EnchantSetting
    {

        public enum LimitedCategory
        {
            Normal = 0,
            Equipment = 10000,
            Item = 30000
        }

        public static readonly string[] EffectName =
        {
            "modify_Atk",
            "modify_AtkRate",
            "modify_Def",
            "modify_DefRate",
            "modify_MAtk",
            "modify_MAtkRate",
            "modify_MaxHp",
            "modify_MaxHpRate",
            "modify_MaxMp",
            "modify_MaxMpRate",
            "modify_MaxSp",
            "modify_MaxSpRate",
            "modify_MaxSt",
            "modify_MaxStRate",
            "modify_CriticalDmgRate",
            "modify_SpConsumeRate",
            "modify_StConsumeRate",
            "modify_ManConsumeRate",
            "modify_StRecoverRate",
            "modify_ManaRecoverRate",
            "modify_SkillCoolDownRate",
            "modify_ItemCoolDownRate",
            "modify_CriticalProb",
            "modify_ItemDropProb",
            "modify_PoisonProb",
            "modify_FireProb",
            "modify_MovementSpeedRate",
            "modify_MotionSpeedRate",
            "modify_JumpSpeedRate",
            "modify_AtkUndead",
            "modify_DefUndead",
            "modify_AtkIce",
            "modify_DefIce",
            "modify_AtkFire",
            "modify_DefFire",
            "modify_AtkBoss",
            "modify_DefBoss",
            "modify_AtkAnimal",
            "modify_DamageCut",
            "modify_JumpCount",
            "modify_PriceRate",
            "use_RestoreHealth",
            "use_RestoreMana",
            "use_RestoreSatiety",
            "equipmentPassiveSkillId",
            "equipmentPassiveSkillId"
        };

        internal float[] probInTreassureBoxes;
        internal float probInStone;
        internal float probInTree;
        internal float probInRandomDrop;
        internal int[] targetEnemyId;
        internal float[] probsInEnemyDrop;
        internal readonly SoEnchantment enchant = ScriptableObject.CreateInstance<BlankEnchantmentSource>();
        private readonly Traverse traverse;

        public int AssignedID
        {
            get
            {
                return enchant.ID;
            }
        }
        

        private EnchantSetting()
        {
            traverse = new Traverse(enchant);
            Status();
            Rarity(0);
            LimitedCategoryId(LimitedCategory.Normal);
            ProbInTreasureBox(new float[EnchantHelper.maxRarity]);
            ProbInStoneDrop(0);
            ProbInTreeDrop(0);
            ProbInRandomDrop(0);
            ProbInEnemyDrop(new int[0], new float[0]);
        }

        public static EnchantSetting Create()
        {
            return new EnchantSetting();
        }

        public EnchantSetting Id(int id)
        {
            if (id <= EnchantHelper.Inst.vanillaLastID)
                throw new ArgumentException(string.Format("id must be more than {0} but {1}", EnchantHelper.Inst.vanillaLastID, id));
            traverse.Field<int>("id").Value = id;
            return this;
        }

        public EnchantSetting NewId()
        {
            return Id(EnchantHelper.Inst.NewId());
        }

        public EnchantSetting Status(int value = 1)
        {
            traverse.Field<int>("status").Value = value;
            return this;
        }

        public EnchantSetting Rarity(EnchantRarity value)
        {
            traverse.Field<EnchantRarity>("rarity").Value = value;
            return this;
        }

        public EnchantSetting LimitedCategoryId(LimitedCategory value)
        {
            traverse.Field<int>("limitedCategoryId").Value = (int)value;
            return this;
        }

        public EnchantSetting LimitedCategoryId(int value)
        {
            traverse.Field<int>("limitedCategoryId").Value = value;
            return this;
        }

        public EnchantSetting Effect(string effectName, float value)
        {
            traverse.Field<float>(effectName).Value = value;
            return this;
        }

        public EnchantSetting Effect(string effectName, int value)
        {
            traverse.Field<int>(effectName).Value = value;
            return this;
        }

        public EnchantSetting ProbInTreasureBox(float[] value)
        {
            if (value.Length != EnchantHelper.maxRarity)
                throw new ArgumentException(string.Format("value's length must be {0} but {1}", EnchantHelper.maxRarity, value.Length));
            this.probInTreassureBoxes = value;
            return this;
        }

        public EnchantSetting ProbInEnemyDrop(int[] targetEnemyId, float[] probs)
        {
            if (targetEnemyId.Length != probs.Length)
                throw new ArgumentException(string.Format(
                    "length must be same but targetEnemyId {0} and probs {1}", 
                    targetEnemyId.Length, 
                    probs.Length ));

            this.targetEnemyId = targetEnemyId;
            this.probsInEnemyDrop = probs;
            return this;
        }

        public EnchantSetting ProbInRandomDrop(float value)
        {
            this.probInRandomDrop = value;
            return this;
        }

        public EnchantSetting ProbInStoneDrop(float value)
        {
            this.probInStone = value;
            return this;
        }

        public EnchantSetting ProbInTreeDrop(float value)
        {
            this.probInTree = value;
            return this;
        }

    }
}
