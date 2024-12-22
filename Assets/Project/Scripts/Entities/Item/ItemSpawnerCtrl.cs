using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Project.Scripts.Core.AssetBundler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Entities.Item
{
  public class ItemSpawnerCtrl : MonoBehaviour
  {
    private const string AssetBundleName = "items";
    private AssetBundle _assetBundle;

    public void Init() { _assetBundle = AssetBundle.LoadFromFile($"{AssetBundlerConstants.Path}/{AssetBundleName}"); }

    public void Spawn(ItemKind itemKind, Vector3 position, [CanBeNull] ItemAsset itemAsset)
    {
      var assetList = _assetBundle.LoadAllAssets<ItemAsset>()
        .Where(a => a.kind == itemKind)
        .ToList();
      var count = assetList.Count();
      if (count <= 0) return;
      var asset = itemAsset ? itemAsset : assetList[Random.Range(0, count)];
      var go = Instantiate(asset.prefab, gameObject.transform);
      go.transform.position = position;
      if (asset.material) go.GetComponent<MeshRenderer>().SetMaterials(new List<Material> { asset.material });
      var ctrl = go.GetComponent<ItemCtrl>();
      ctrl.asset = Instantiate(asset);
      ctrl.asset.guid = Guid.NewGuid().ToString();
      go.name = $"{itemKind.ToString()}.{ctrl.asset.guid}";
    }
  }
}