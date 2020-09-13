# LibCraftopia
A  unofficial modding library for Craftopia (https://store.steampowered.com/app/1307550/Craftopia/)

# Setup

This is a mod library based on BepInEx. Follow the install instruction of BepInEx, https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html. 


Download https://github.com/nanofi/LibCraftopia/releases/download/v0.1.0/LibCraftopia.dll and add a reference to `LibCraftopia.dll` to your project. Then, add the `BepInDependency` attribute  to your plug-in class.

```csharp
[BepInPlugin("your guid", "your mod name", "your mod version")]
[BepInDependency("com.craftopia.mod.LibCraftopia", BepInDependency.DependencyFlags.HardDependency)] // Add this!
```
# Usage

See source code...

# Remark

You must place `LibCraftopia.dll` on the `plugins` folder of BepInEx. 

# Changelog

- 2020/09/13 v0.1.0
