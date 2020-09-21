
# Contribution Guide

## Pull Request

- I'd like to keep LibCraftopia merely as a library so that this library by itself does not make a dramatic change into the game. Of course, this library can have features to change the game dramatically, whereas the change must be done by mod developers who use this library. Please allow me to reject your PR by this reason. 
- You should be aware before submitting PR that your code will publish under the MIT license after accepting your PR.


## Development

To build this library, you need to place the following dlls on the `Libs` directory, which can be found in the `Craftopia_Data/Managed` directory or `BeplnEx/core` directory.
- 0Harmony.dll
- Assembly-CSharp.dll
- BeplnEx.dll
- BeplnEx.Harmony.dll
- UnityEngine.CoreModule.dll
- UnityEngine.dll
- UnityEngine.ImageConversionModule.dll
- UnityEngine.UI.dll

We use [GitInfo](https://www.nuget.org/packages/GitInfo/) for automatically configuring the assembly version and mod version from the git tag and commit history. 