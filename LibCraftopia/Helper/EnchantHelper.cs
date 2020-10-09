using HarmonyLib;
using Oc;
using Oc.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using LibCraftopia.Enchant;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Helper
{
    public class EnchantHelper : SingletonMonoBehaviour<EnchantHelper>
    {
        protected override void OnUnityAwake()
        {
            MaxRarity = AccessTools.StaticFieldRefAccess<SoEnchantDataList, int>("MaxRarity");

            UnspecifiedEnemyDrop = new Dictionary<int, float>();
            SpecifiedEnemyDrop = new Dictionary<int, Dictionary<int, float>>();
            TreeRandomDrop = new Dictionary<int, float>();
            StoneRandomDrop = new Dictionary<int, float>();
        }

        public int MaxRarity { get; private set; }

        //The number of enchantments an item or enemy can hold is limited, so hold them separately.
        public Dictionary<int, float> UnspecifiedEnemyDrop { get; private set; }
        public Dictionary<int, Dictionary<int, float>> SpecifiedEnemyDrop { get; private set; }
        public Dictionary<int, float> TreeRandomDrop { get; private set; }
        public Dictionary<int, float> StoneRandomDrop { get; private set; }

    }
}
