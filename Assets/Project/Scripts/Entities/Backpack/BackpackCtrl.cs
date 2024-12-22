using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Entities.Item;
using Project.Scripts.Scenes.MainScene;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Entities.Backpack
{
  public class BackpackCtrl : MonoBehaviour
  {
    public MainScene sceneCtrl;
    public BackpackAsset asset;
    public GameObject lanternGo;

    private BackpackRemoteRepoCtrl _backpackRemoteRepo;

    private void Start()
    {
      _backpackRemoteRepo = gameObject.GetComponent<BackpackRemoteRepoCtrl>();
    }

    public void OnMouseDown() { sceneCtrl.OpenBackpackFrm(); }

    public void Init()
    {
      asset = ScriptableObject.CreateInstance<BackpackAsset>();
      asset.itemAssetList = new List<ItemAsset>();
      lanternGo.SetActive(false);
    }

    public void AddItem(ItemAsset itemAsset)
    {
      _backpackRemoteRepo.CreateEvent(new BackpackRemoteRepoEvent()
      {
        itemKind = itemAsset.kind,
        itemGuid = itemAsset.guid,
        eventKind = ItemEventKind.AddToBackpack
      });
      switch (itemAsset.kind)
      {
        case ItemKind.Lantern:
          asset.lanternItemAsset = itemAsset;
          lanternGo.SetActive(true);
          break;
        default:
          asset.itemAssetList.Add(itemAsset);
          break;
      }
    }

    public void RemoveItem(string guid, ItemKind kind)
    {
      _backpackRemoteRepo.CreateEvent(new BackpackRemoteRepoEvent()
      {
        itemKind = kind,
        itemGuid = guid,
        eventKind = ItemEventKind.RemoveFromBackpack
      });
      switch (kind)
      {
        case ItemKind.Lantern:
          asset.lanternItemAsset = null;
          lanternGo.SetActive(false);
          break;
        default:
          var itemAsset = asset.itemAssetList.Single(itemAsset => itemAsset.guid == guid && itemAsset.kind == kind);
          if (itemAsset) asset.itemAssetList.Remove(itemAsset);
          break;
      }
    }
  }
}