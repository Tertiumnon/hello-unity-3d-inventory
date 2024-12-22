using System.IO;
using Project.Scripts.Core.AssetBundler;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Editor
{
  public class AssetBundler : MonoBehaviour
  {
    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAll()
    {
      if (!Directory.Exists(AssetBundlerConstants.Path)) Directory.CreateDirectory(AssetBundlerConstants.Path);
      BuildPipeline.BuildAssetBundles(
        AssetBundlerConstants.Path,
        BuildAssetBundleOptions.None,
        BuildTarget.StandaloneWindows
      );
    }
  }
}