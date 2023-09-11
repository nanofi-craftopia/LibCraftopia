using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class AssetBundleBuild 
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        var targetDir = "Build/AssetBundles";
        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }
        BuildPipeline.BuildAssetBundles(targetDir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
