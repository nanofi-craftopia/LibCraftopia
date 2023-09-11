# LibCraftopia
An unofficial modding library for Craftopia (https://store.steampowered.com/app/1307550/Craftopia/)

[![GitHub Releases (by Asset)](https://img.shields.io/github/downloads/nanofi/LibCraftopia/latest/LibCraftopia.dll)](https://github.com/nanofi/LibCraftopia/releases/latest/download/LibCraftopia.dll)
[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/nanofi/LibCraftopia?include_prereleases)](https://github.com/nanofi/LibCraftopia/releases/latest)
[![GitHub](https://img.shields.io/github/license/nanofi/LibCraftopia)](https://github.com/nanofi/LibCraftopia/blob/master/LICENSE)

# Setup

This is a mod library based on BepInEx. Follow the install instruction of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html. Also, follow the tutorial of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/dev_guide/plugin_tutorial/index.html, to create your BepInEx mod.


Download the `LibCraftopia.dll` from the above download badge and add a reference to the downloaded dll file to your project. Then, add the `BepInDependency` attribute to your plug-in class.

```csharp
[BepInPlugin("your guid", "your mod name", "your mod version")]
[BepInDependency(LibCraftopia.LibCraftopia.GUID, BepInDependency.DependencyFlags.HardDependency)] // Add this!
```

# Usage

# Initialization

You can add your initialization procedure as follows:
```csharp
public class YourMod : IInitializeHandler {
    void Start() {
        InitializeManager.Inst.AddHandler(InitializeManager.ModInit, this);
    }

    public async UniTask Init(InitializeContext context) {
        // Do your initialization
    }
}
```

`Init` method will be called during the startup initialization. `InitializeContext` can be used for controlling the information displayed in the startup scene.

# Registry

A registry is a manager for game element, such as item, enchant, skill, enemy, and so on (currently, the item and enchant APIs are provided), by which we can keep the consistency of the added game elements' ids against update of the official game and mods. When adding a game element via the registry, you must specify a unique string as the game element's unique key instead of specifying the game element's id. The registry assigns an unused id to the game element automatically and remembers the correspondences between the string and id. 

Accesses of the registry APIs must be done in `Init` method. What you should do first to access the registry APIs is obtaining a registry via `RegistryManager.GetRegistry<T>()`. For example, you can obtain an item registry as
```csharp
public class YourMod : IInitializeHandler {
    void Start() {
        InitializeManager.Inst.AddHandler(InitializeManager.ModInit, this);
    }

    public async UniTask Init(InitializeContext context) {
        var itemRegistry = RegistryManager.Inst.GetRegistry<Item>();

        // Add items
    }
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
enchantRegistry.Register("myenchant.MyEnchant", enchant);
```

# Changelog

See [CHANGELOG.md](CHANGELOG.md)

