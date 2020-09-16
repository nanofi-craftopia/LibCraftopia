using HarmonyLib;
using LibCraftopia.Helper;
using Oc;
using Oc.Item;
using Oc.UIDatas;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemBuilder
    {
        private readonly ItemData item = ScriptableObject.CreateInstance<ItemData>();
        private readonly Traverse traverse;
        private readonly List<Tuple<int, int>> materials = new List<Tuple<int, int>>();

        private ItemBuilder()
        {
            traverse = new Traverse(item);
            Status();
            MaxStack(99);
            Price(1);
            Rarity(1);
            PlayerCraftCount(1);
            CraftTimeCost(3);
            CooldownTime(0);
            EffectiveTime(0);
        }

        public static ItemBuilder Create()
        {
            return new ItemBuilder();
        }

        public ItemBuilder Id(int id)
        {
            traverse.Field<int>("id").Value = id;
            traverse.Field<int>("pKey").Value = id;
            return this;
        }
        public ItemBuilder NewId()
        {
            return Id(ItemHelper.Inst.NewId());
        }
        public ItemBuilder NewId(out int id)
        {
            var builder = NewId();
            id = builder.item.Id;
            return builder;
        }

        public ItemBuilder DevPriority(int value)
        {
            traverse.Field<int>("devPriority").Value = value;
            return this;
        }

        public ItemBuilder Status(int value = 1)
        {
            traverse.Field<int>("status").Value = value;
            return this;
        }

        public ItemBuilder Icon(Sprite icon)
        {
            item.Set(icon);
            return this;
        }

        public ItemBuilder MaxStack(int value)
        {
            traverse.Field<int>("maxStack").Value = value;
            return this;
        }

        public ItemBuilder Price(int value)
        {
            traverse.Field<int>("price").Value = value;
            return this;
        }

        public ItemBuilder ProbInTreasureBox(float value)
        {
            traverse.Field<float>("probInTreasureBox").Value = value;
            return this;

        }
        public ItemBuilder Rarity(int value)
        {
            traverse.Field<int>("rarity").Value = value;
            return this;
        }
        public ItemBuilder DisplayRarity(int value)
        {
            traverse.Field<int>("displayRarity").Value = value;
            return this;
        }
        public ItemBuilder DisplayRarity(ItemRarity value)
        {
            traverse.Field<int>("displayRarity").Value = (int)value;
            return this;
        }

        public ItemBuilder CategoryId(int value)
        {
            traverse.Field<int>("categoryId").Value = value;
            return this;
        }
        public ItemBuilder Category(ItemCategory value)
        {
            traverse.Field<int>("categoryId").Value = (int)value;
            return this;
        }
        public ItemBuilder FamilyId(int value)
        {
            traverse.Field<int>("familyId").Value = value;
            return this;
        }

        public ItemBuilder InventryTabId(int value)
        {
            traverse.Field<int>("inventoryTabId").Value = value;
            return this;
        }
        public ItemBuilder ItemType(ItemType value)
        {
            traverse.Field<int>("inventoryTabId").Value = (int)value;
            return this;
        }

        public ItemBuilder WorkbenchId(int value)
        {
            traverse.Field<int>("workbenchId").Value = value;
            return this;
        }
        public ItemBuilder WorkbenchLevel(int value)
        {
            traverse.Field<int>("workbenchLv").Value = value;
            return this;
        }
        public ItemBuilder Workbench(int id, int level)
        {
            return this.WorkbenchId(id).WorkbenchLevel(level);
        }

        public ItemBuilder PlayerCraftCount(int value)
        {
            traverse.Field<int>("playerCraftCount").Value = value;
            return this;
        }
        public ItemBuilder CraftTimeCost(float value)
        {
            traverse.Field<float>("carftTimeCost").Value = value;
            return this;
        }
        public ItemBuilder Material(int id, int count)
        {
            materials.Add(Tuple.Create(id, count));
            return this;
        }
        public ItemBuilder CooldownTime(float value)
        {
            traverse.Field<float>("cooldownTime").Value = value;
            return this;
        }

        public ItemBuilder EffectiveTime(float value)
        {
            traverse.Field<float>("effectiveTime").Value = value;
            return this;
        }
        public ItemBuilder RestoreHealth(float value)
        {
            traverse.Field<float>("restoreHealth").Value = value;
            return this;
        }
        public ItemBuilder RestoreMana(float value)
        {
            traverse.Field<float>("restoreMana").Value = value;
            return this;
        }
        public ItemBuilder RestoreSatiety(float value)
        {
            traverse.Field<float>("restoreSatiety").Value = value;
            return this;
        }
        public ItemBuilder RestoreStamina(float value)
        {
            traverse.Field<float>("addStamina").Value = value;
            return this;
        }

        public ItemBuilder Restore(float health, float mana, float satiety, float stamina)
        {
            return RestoreHealth(health).RestoreMana(mana).RestoreSatiety(satiety).RestoreStamina(stamina);
        }

        public ItemBuilder ATK(int value, int increasePerLevel)
        {
            traverse.Field<int>("atk").Value = value;
            traverse.Field<int>("atkIncreasePerLv").Value = increasePerLevel;
            return this;
        }
        public ItemBuilder DEF(int value, int increasePerLevel)
        {
            traverse.Field<int>("def").Value = value;
            traverse.Field<int>("defIncreasePerLv").Value = increasePerLevel;
            return this;
        }
        
        public ItemBuilder MATK(int value, int increasePerLevel)
        {
            traverse.Field<int>("matk").Value = value;
            traverse.Field<int>("matkIncreasePerLv").Value = increasePerLevel;
            return this;
        }
        public ItemBuilder MotionSpeed(int value)
        {
            traverse.Field<int>("motionSpeed").Value = value;
            return this;
        }
        public ItemBuilder DurabilityValue(float value)
        {
            traverse.Field<float>("durabilityValue").Value = value;
            return this;
        }
        public ItemBuilder RegistFireLv(int value)
        {
            traverse.Field<int>("registFireLv").Value = value;
            return this;
        }
        public ItemBuilder RegistColdLv(int value)
        {
            traverse.Field<int>("registColdLv").Value = value;
            return this;
        }
        public ItemBuilder RegistPoisonLv(int value)
        {
            traverse.Field<int>("registPoisonLv").Value = value;
            return this;
        }

        public ItemBuilder RegistLv(int fire, int cold, int poison)
        {
            return RegistFireLv(fire).RegistColdLv(cold).RegistPoisonLv(poison);
        }

        public ItemBuilder PassiveSkill(int id, int level)
        {
            traverse.Field<int>("equipmentPassiveSkillId").Value = id;
            traverse.Field<int>("equipmentPassiveSkillLevel").Value = level;
            return this;
        }
        public ItemBuilder ActiveSkill(int id, int level)
        {
            traverse.Field<int>("equipmentActiveSkillId").Value = id;
            traverse.Field<int>("equipmentActiveSkillLevel").Value = level;
            return this;
        }
        public ItemBuilder ExtraValueA(float value)
        {
            traverse.Field<float>("extraValue_A").Value = value;
            return this;
        }
        public ItemBuilder ExtraValueB(float value)
        {
            traverse.Field<float>("extraValue_B").Value = value;
            return this;
        }

        public ItemBuilder Handler(ItemHandlerSO value)
        {
            traverse.Field<ItemHandlerSO>("handler").Value = value;
            return this;
        }
        public ItemBuilder ItemPrefab(OcItemPrefab value)
        {
            traverse.Field<OcItemPrefab>("itemPrefab").Value = value;
            return this;
        }
        public ItemBuilder ActiveSkillType(OcPlActiveSkillType value)
        {
            traverse.Field<OcPlActiveSkillType>("activeSkillType").Value = value;
            return this;
        }
        public ItemBuilder PassiveSkillType(OcPlPassiveSkillType value)
        {
            traverse.Field<OcPlPassiveSkillType>("passiveSkillType").Value = value;
            return this;
        }

        public ItemData Build()
        {
            buildMaterials();
            return item;
        }

        private void buildMaterials()
        {
            for (int i = 0; i < 5; i++)
            {
                if (i >= materials.Count) break;
                var pair = materials[i];
                var name = "material" + (char)((int)'A' + i);
                traverse.Field<int>(name + "_Id").Value = pair.Item1;
                traverse.Field<int>(name + "_count").Value = pair.Item2;

            }
        }
    }
}
