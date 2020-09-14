using BepInEx.Logging;
using I2.Loc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibCraftopia.Helper
{
    public class LocalizationHelper : SingletonMonoBehaviour<LocalizationHelper>
    {
        protected override void OnUnityAwake()
        {
            EnglishIndex = LocalizationManager.Sources[0].GetLanguageIndex(English);
            JapaneseIndex = LocalizationManager.Sources[0].GetLanguageIndex(Japanese);
            ChineseSimplifiedIndex = LocalizationManager.Sources[0].GetLanguageIndex(ChineseSimplified);
            ChineseTraditionalIndex = LocalizationManager.Sources[0].GetLanguageIndex(ChineseTraditional);
        }

        public const string English = "English";
        public const string Japanese = "Japanese";
        public const string ChineseSimplified = "Chinese (Simplified)";
        public const string ChineseTraditional = "Chinese (Traditional)";
        public int EnglishIndex { get; private set; }
        public int JapaneseIndex { get; private set; }
        public int ChineseSimplifiedIndex { get; private set; }
        public int ChineseTraditionalIndex { get; private set; }


        public TermData AddTerm(string term)
        {
            return LocalizationManager.Sources[0].AddTerm(term);
        }
        public TermData AddUI(string path)
        {
            return AddTerm(string.Format("100_UI/{0}", path));
        }
        public string GetUI(string path, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("100_UI/{0}", path), true, 0, true, false, null, overrideLanguage);
        }

        public TermData AddItemDisplayName(int id)
        {
            return AddTerm(string.Format("200_ItemName/{0}", id));
        }
        public TermData AddItemFamily(int id)
        {
            return AddTerm(string.Format("201_ItemFamily/{0}", id));
        }
        public TermData AddItemCategory(int id)
        {
            return AddTerm(string.Format("202_ItemCategory/{0}", id));
        }
        public TermData AddItemDescription(int id)
        {
            return AddTerm(string.Format("203_ItemDescription/{0}", id));
        }
        public string GetItemDisplayName(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("200_ItemName/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetItemFamily(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("201_ItemFamily/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetItemCategory(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("202_ItemCategory/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetItemDescription(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("203_ItemDescription/{0}", id), true, 0, true, false, null, overrideLanguage);
        }

        public TermData AddEnchantDisplayName(int id)
        {
            return AddTerm(string.Format("300_EnchantName/{0}", id));
        }
        public TermData AddEnchantDescription(int id)
        {
            return AddTerm(string.Format("301_EnchantDesc/{0}", id));
        }
        public string GetEnchantDisplayName(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("300_EnchantName/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetEnchantDescription(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("301_EnchantDesc/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public TermData AddEnemyName(int id)
        {
            return AddTerm(string.Format("400_Enemy/{0}", id));
        }
        public string GetEnemyName(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("400_Enemy/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public TermData AddJobTitle(int id)
        {
            return AddTerm(string.Format("500_JobTitle/{0}", id));
        }
        public string GetJobTitle(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("500_JobTitle/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public TermData AddNPCLine(int id)
        {
            return AddTerm(string.Format("600_NPCLine/{0}", id));
        }
        public string GetNPCLine(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("600_NPCLine/{0}", id), true, 0, true, false, null, overrideLanguage);
        }

        public TermData AddMissionOriginTitle(int id)
        {
            return AddTerm(string.Format("700_MissionName/{0}", id));
        }
        public TermData AddMissionOriginDesc(int id)
        {
            return AddTerm(string.Format("701_MissionDesc/{0}", id));
        }
        public TermData AddMissionCategory(int id)
        {
            return AddTerm(string.Format("702_MissionCategory/{0}", id));
        }
        public string GetMissionOriginTitle(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("700_MissionName/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetMissionOriginDesc(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("701_MissionDesc/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
        public string GetMissionCategory(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("702_MissionCategory/{0}", id), true, 0, true, false, null, overrideLanguage);
        }

        public TermData AddDungeonName(int id)
        {
            return AddTerm(string.Format("800_DungeonName/{0}", id));
        }
        public string GetDungeonName(int id, string overrideLanguage = null)
        {
            return LocalizationManager.GetTranslation(string.Format("800_DungeonName/{0}", id), true, 0, true, false, null, overrideLanguage);
        }
    }
}
