
# Contribution Guide

## Pull Request

- I'd like to keep LibCraftopia merely as a library so that this library by itself does not make a dramatic change into the game. Of course, this library can have features to change the game dramatically, whereas the change must be done by mod developers who use this library. Please allow me to reject your PR by this reason. 
- You should be aware before submitting PR that your code will publish under the MIT license after accepting your PR.


## Development

To build this library, you need to place the following dlls on the `Libs` directory, which can be found in the `Craftopia_Data/Managed` directory or `BeplnEx/core` directory.
- 0Harmony.dll
- AD__Overcraft.dll
- AD_External_i2.dll
- BeplnEx.dll
- BeplnEx.Harmony.dll
- UnityEngine.CoreModule.dll
- UnityEngine.dll
- UnityEngine.ImageConversionModule.dll
- UnityEngine.UI.dll

### Visual Studio Packages
We use [GitInfo](https://www.nuget.org/packages/GitInfo/) for automatically configuring the assembly version and mod version from the git tag and commit history. 

### NPM Packages for releasing

We use [semantic-release](https://github.com/semantic-release/semantic-release) to release built assemblies to Github. 

Note: the LTS and latest version of nodejs (12.19.0 and 14.14.0) seems to have a bug in executing a process in a windows environment. I use the nightly version to avoid that. 