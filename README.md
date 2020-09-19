# LibCraftopia
A  unofficial modding library for Craftopia (https://store.steampowered.com/app/1307550/Craftopia/)

# Setup

This is a mod library based on BepInEx. Follow the install instruction of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html. 


Download https://github.com/nanofi/LibCraftopia/releases/download/v0.1.4/LibCraftopia.dll and add a reference to `LibCraftopia.dll` to your project. Then, add the `BepInDependency` attribute  to your plug-in class.

```csharp
[BepInPlugin("your guid", "your mod name", "your mod version")]
[BepInDependency("com.craftopia.mod.LibCraftopia", BepInDependency.DependencyFlags.HardDependency)] // Add this!
```
# Usage

# Initialization

You can add your initialization procedure as follows:
```csharp
void Start() {
    LoadingManager.Inst.InitializeLoaders.Add(10, initCoroutine); // 10 is priority. Smaller coroutine will be called earlyer. 
    LoadingManager.Inst.InitializeGameLoaders.Add(10, initGameCoroutine);
}

private IEnumerator initCoroutine(bool needStabilization) {
    // Do your initialization, e.g., add items and add enchants
    // Note that this is coroutine
}

private IEnumerator initGameCoroutine(bool needStabilization) {
    // Do your initialization, e.g., add skills and add missions
    // Some of the game contents, such as skill and mission, are destroyed when the game scene is destroyed (when going back to the title scene). 
    // This means that if you want to modify these game contents, you need to modify them whenever the game scene is loaded.
    // Coroutines added to `InitializeGameLoaders` will be called immediately after starting the game scene's loading. 
    // Note that this is coroutine
}
```

#  Add an item

```csharp
var icon = ...; // Icon Sprite
var familyId = ItemHelper.Inst.NewFamilyId();
var item = ItemBuilder.Create()
  .NewId()
  .Icon(icon)
  .Category(ItemCategory._28_Potion)
  .FamilyId(familyId)
  .ItemType(ItemType.Consumption)
  .Workbench(20011, 1) 
  .Material(1218, 1)
  .CooldownTime(1)
  .RestoreHealth(200)
  .Handler(ItemHelper.Inst.PotionHandler)
  .Build();
ItemHelper.Inst.AddItems(item);
```

#  the display name of the added item

```csharp
var displayName = LocalizationHelper.Inst.AddItemDisplayName(item.id);
displayName.Languages[LocalizationHelper.Inst.EnglishIndex] = "Peach potion";
displayName.Languages[LocalizationHelper.Inst.JapaneseIndex] = "ピーチポーション";
displayName.Languages[LocalizationHelper.Inst.ChineseSimplifiedIndex] = "..."; // Sorry, i cannot write chinese
displayName.Languages[LocalizationHelper.Inst.ChineseTraditionalIndex] = "...";
```

#  Add an Enchant

```csharp
var enchant = LibCraftopia.Enchant.EnchantSetting.Create();
enchant.NewId();
enchant.Rarity(Oc.Item.EnchantRarity.Rare);
enchant.LimitedCategoryId(EnchantSetting.LimitedCategory.Equipment);
enchant.Effect(EnchantSetting.EffectName[0], 100);
enchant.ProbInTreasureBox(new float[5] { 0, 0, 3, 3, 3});
enchant.ProbInStoneDrop(0.8f);
enchant.ProbInTreeDrop(0.8f);
enchant.ProbInEnemyDrop(new int[] { 22, 23 }, new float[] { 0.5f, 0.1f });
enchant.ProbInRandomDrop(0.3f);
EnchantHelper.Inst.AddEnchant(enchant);
```

# Add a chat command

```csharp
ChatCommandManager.Inst.Commands.Add(new MyCommand());
```
where `MyCommand` class is implemented as
```csharp
        private class MyCommand : IChatCommandWithSubs
        {
            private class HelloCommand : IChatCommand
            {
                public string Command => "hello";
                public void Invoke(string[] args)
                {
                    ChatCommandManager.Inst.PopMessage("Hello: {0}", args.Join());
                }

            }
            public string Command => "mycommand";

            public void Invoke(string[] args)
            {
                ChatCommandManager.Inst.PopMessage("Usage: mycommand (hello)");
            }

            public IChatCommand Subcommand(string command)
            {
                switch (command)
                {
                    case "hello": return new HelloCommand();
                    default: return null; // MyCommand.Invoke will be called
                }
            }
        }
```

In the game, entering `/mycommand` in the chat input will show
```
Usage: mycommand (hello)
```
Entering `/mycommand hello this is a test "this is a test"` will show
```
Hello: this, is, a, test, this is a test
```

# Remark

You must place `LibCraftopia.dll` on the `plugins` folder of BepInEx. 

# Changelog

- 2020/09/19 v0.1.4 Add loading feature + Bug fix
- 2020/09/18 v0.1.3 Add chat command feature
- 2020/09/17 v0.1.2
- 2020/09/17 v0.1.1 
- 2020/09/13 v0.1.0

# Acknowledgements
- mituha : The chat command feature is inspired from https://github.com/mituha/CraftopiaPlugins/blob/main/ConsoleCommandsPlugin/ConsoleCommandPlugin.cs
