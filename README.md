# LibCraftopia
An unofficial modding library for Craftopia (https://store.steampowered.com/app/1307550/Craftopia/)

[![GitHub Releases (by Asset)](https://img.shields.io/github/downloads/nanofi/LibCraftopia/latest/LibCraftopia.dll)](https://github.com/nanofi/LibCraftopia/releases/latest/download/LibCraftopia.dll)
[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/nanofi/LibCraftopia?include_prereleases)](https://github.com/nanofi/LibCraftopia/releases/latest)
[![GitHub](https://img.shields.io/github/license/nanofi/LibCraftopia)](https://github.com/nanofi/LibCraftopia/blob/master/LICENSE)

# Setup

This is a mod library based on BepInEx. Follow the install instruction of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html. Also, follow the tutorial of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/dev_guide/plugin_tutorial/index.html, to create your BepInEx mod.


Download the `LibCraftopia.dll` from the above download badge and add a reference to the downloaded dll file to your project. Then, add the `BepInDependency` attribute  to your plug-in class.

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

Coroutines added to `InitializeLoaders` or `InitializeGameLoaders` will be called during the loading scene. By appropriate implementation of the coroutines, we can avoid freezing the game application. The difference between `InitializeLoaders` and `InitializeGameLoaders` is that while coroutines added to `InitializeLoaders` will be called once the game scene is loaded, coroutines added to `InitializeGameLoaders` will be called each time the game scene is loaded. For example, when we go back to the title scene and start the game again, coroutines in `InitializeLoaders` are not called, whereas that in `InitializeGameLoaders` are called.

# Registry

A registry is a manager for game element, such as item, enchant, skill, enemy, and so on (currently, the item and enchant APIs are provided), by which we can keep the consistency of the added game elements' ids against update of the official game and mods. When adding a game element via the registry, you must specify a unique string as the game element's unique key instead of specifying the game element's id. The registry assigns unused id to the game element automatically and remembers the correspondences between the string and id. 

Accesses of the registry APIs must be done in coroutines added to the `InitializeLoaders` with priority grater than 20. What you should do first to access the registry APIs is obtaining a registry via `RegistryManager.GetRegistry<T>()`. For example, you can obtain an item registry as
```csharp
void Start() {
    LoadingManager.Inst.InitializeLoaders.Add(50, initCoroutine);
}

private IEnumerator initCoroutine(bool needStabilization) {
    var itemRegistry = RegistryManager.Inst.GetRegistry<Item>();

    // Add items
}
```

You can add a game element through `Registry<T>.Register(string key, T element)`. The parameter `key` is the unique string of the game element. For example, 
```csharp
Item item = ...; // initialize an item
itemRegistry.Register("your.mod.specific.guid.ExampleItem", item);
```

Other registry APIs:
```csharp
var copperIngot = itemRegistry.GetElement("CopperIngod");
if(itemRegistry.ExistsKey("your.mod.specific.guid.ExampleItem")) {
    UnityEngine.Debug.LogInfo("An item with the key `your.mod.specific.guid.ExampleItem` exists.");
}
itemRegistry.Unregister("your.mod.specific.guid.ExampleItem");
```

Currently, we provide registries for the following types of game elements.
| Game element | Type                           |
| ------------ | ------------------------------ |
| Item         | `LibCraftopia.Item.Item`       |
| ItemFamily   | `LibCraftopia.Item.ItemFamily` |
| Enchant      | `LibCraftopia.Enchant.Enchant` |

#  Add an item

```csharp
var icon = ...; // Icon Sprite
var familyId = ...; // Item family id
var item = new Item();
item.Icon = icon;
item.Category = ItemCategory._28_Potion;
item.FamilyId = familyId;
item.ItemType = ItemType.Consumption;
item.WorkbenchId = 20011;
item.WorkbenchLevel = 1;
item.MaterialAId = 1218;
item.MaterialACount = 1;
item.CooldownTime = 1;
item.RestoreHealth = 200;
item.Handler = ItemHelper.Inst.PotionHandler;
itemRegistry.Register("peachpotion.PeachPotion", item);
```

#  Localize the display name of the added item

```csharp
var displayName = LocalizationHelper.Inst.AddItemDisplayName(item.Id); // The registry assigns `Id` of an item automatically when registering the item.
displayName.Languages[LocalizationHelper.Inst.EnglishIndex] = "Peach potion";
displayName.Languages[LocalizationHelper.Inst.JapaneseIndex] = "ピーチポーション";
displayName.Languages[LocalizationHelper.Inst.ChineseSimplifiedIndex] = "..."; // Sorry, i cannot write chinese
displayName.Languages[LocalizationHelper.Inst.ChineseTraditionalIndex] = "...";
```

#  Add an Enchant

```csharp
var enchant = new Enchant();
enchant.Rarity = Oc.Item.EnchantRarity.Rare;
enchant.LimitedCategoryId = (int)EnchantLimitedCategory.Equipment;
enchant[EnchantEffect.modify_Atk] = 100;
enchant.ProbInTreasureBox = new float[] { 0, 0, 3, 3, 3};
enchant.ProbInStoneDrop = 0.8f;
enchant.ProbInTreeDrop = 0.8f;
enchant.ProbInEnemyDrop[22] = 0.5f;
enchant.ProbInEnemyDrop[23] = 0.1f;
enchant.ProbInRandomDrop = 0.3f;
enchantRegistry.Register("myenchant.MyEnchant", enchant);
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

- 2020/09/25 v0.1.8 Add a way to configure the minimum id of user added game elements
- 2020/09/24 v0.1.7 Fix for the latest game
- 2020/09/24 v0.1.6 Add registry feature + Bug fix
- 2020/09/21 v0.1.5 Fix bug in loading feature
- 2020/09/19 v0.1.4 Add loading feature + Bug fix
- 2020/09/18 v0.1.3 Add chat command feature
- 2020/09/17 v0.1.2
- 2020/09/17 v0.1.1 
- 2020/09/13 v0.1.0

# Acknowledgements
- mituha : The chat command feature is inspired from https://github.com/mituha/CraftopiaPlugins/blob/main/ConsoleCommandsPlugin/ConsoleCommandPlugin.cs
