
# Contribution Guide

## Pull Request

- I intend to maintain LibCraftopia strictly as a library, ensuring that it doesn't introduce significant alterations to the game by itself. While the library can certainly offer features capable of dramatic game changes, the implementation of such changes should be the responsibility of mod developers who utilize this library. For this reason, I must decline your pull request.
- Before submitting your pull request, please be aware that your code will be published under the MIT license once your pull request is accepted.


## Development

To build this library, place the required DLL files in the Libs folder, which is located either in the `Craftopia_Data/Managed` directory or the `BepInEx/core` directory.
- 0Harmony.dll
- AD__Overcraft.dll
- AD_External_i2.dll
- AD_PpLib.dll
- BeplnEx.dll
- BeplnEx.Harmony.dll
- UnityEngine.CoreModule.dll
- UnityEngine.dll
- UnityEngine.ImageConversionModule.dll
- UnityEngine.UI.dll
- UnityEngine.UIElementsModule.dll
- UnityEngine.AssetBundleModule.dll
- UnityEngine.UnityWebRequestModule.dll
- Mirror.dll
- UniTask.dll
- UniTask.Linq.dll

### Visual Studio Packages
We utilize [GitInfo](https://www.nuget.org/packages/GitInfo/) to automatically configure both the assembly version and module version based on git tags and commit history.

### NPM Packages for releasing

We utilize [semantic-release](https://github.com/semantic-release/semantic-release) to publish built assemblies on GitHub.
