using Project.Scripts.Entities.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Entities.Backpack
{
  public class BackpackFrmCtrl : MonoBehaviour
  {
    private const int ItemsPerRow = 5;
    private const float ImgWidth = 32f;
    private const float ImgHeight = 32f;
    private const float CellMargin = 5f;
    private const float RowMargin = 5f;
    public BackpackCtrl backpackCtrl;
    public GameObject scrollViewViewportGo;
    public GameObject scrollViewContentGo;
    public GameObject lanternItemGo;
    public GameObject itemImgGo;
    private ItemAsset _hoveredItemAsset;

    private GameObject _scrollViewContentGoInst;

    private void Start()
    {
      itemImgGo.SetActive(false);
      scrollViewContentGo.SetActive(false);
    }

    public void Init()
    {
      itemImgGo.SetActive(false);
      DrawLanternInventoryItem();
      DrawOtherInventoryItemList();
    }

    public void OnListItemMousePointerEnter(GameObject itemGo)
    {
      var itemRefCtrl = itemGo.GetComponent<ItemRefCtrl>();
      switch (itemRefCtrl.kind)
      {
        case ItemKind.Lantern:
          _hoveredItemAsset = backpackCtrl.asset.lanternItemAsset;
          break;
        default:
          _hoveredItemAsset = backpackCtrl.asset.itemAssetList.Find(
            itemAsset => itemAsset.guid == itemRefCtrl.guid && itemAsset.kind == itemRefCtrl.kind
          );
          break;
      }
    }

    public void OnListItemMousePointerExit(GameObject itemGo) { _hoveredItemAsset = null; }

    public ItemAsset GetHoveredListItemAsset() { return _hoveredItemAsset; }

    private void DrawLanternInventoryItem()
    {
      var asset = backpackCtrl.asset.lanternItemAsset;
      if (asset)
      {
        var lanternItemCtrl = lanternItemGo.GetComponent<ItemRefCtrl>();
        lanternItemCtrl.guid = asset.guid;
        lanternItemCtrl.kind = asset.kind;
        if (asset.image) lanternItemGo.GetComponent<Image>().sprite = asset.image;
      }
      lanternItemGo.SetActive(asset);
    }

    private void DrawOtherInventoryItemList()
    {
      if (_scrollViewContentGoInst) Destroy(_scrollViewContentGoInst);
      _scrollViewContentGoInst = Instantiate(scrollViewContentGo, scrollViewViewportGo.transform);
      var itemAssetList = backpackCtrl.asset.itemAssetList;
      var itemAssetListCount = itemAssetList.Count;
      var currentItemImgGoPos = itemImgGo.transform.position;
      for (var i = 0; i < itemAssetListCount; i++)
      {
        var itemAsset = itemAssetList[i];
        var newItemImgGoInst = Instantiate(itemImgGo, _scrollViewContentGoInst.transform);
        currentItemImgGoPos = new Vector2(
          currentItemImgGoPos.x + (i % ItemsPerRow == 0 ? 0 : ImgWidth + CellMargin),
          currentItemImgGoPos.y
        );
        if (i > 0 && i % ItemsPerRow == 0)
          currentItemImgGoPos = new Vector2(
            itemImgGo.transform.position.x,
            currentItemImgGoPos.y - (ImgHeight + RowMargin)
          );
        newItemImgGoInst.transform.position = currentItemImgGoPos;
        var itemRefCtrl = newItemImgGoInst.GetComponent<ItemRefCtrl>();
        itemRefCtrl.guid = itemAsset.guid;
        itemRefCtrl.kind = itemAsset.kind;
        if (itemAsset.image) newItemImgGoInst.GetComponent<Image>().sprite = itemAsset.image;
        newItemImgGoInst.SetActive(true);
      }
      _scrollViewContentGoInst.SetActive(true);
    }
  }
}