using System;
using Project.Scripts.Entities.Backpack;
using Project.Scripts.Entities.Item;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Project.Scripts.Scenes.MainScene
{
  public class MainScene : MonoBehaviour
  {
    public GameObject uiGo;
    public ItemSpawnerCtrl itemSpawnerCtrl;
    public GameObject playerGo;
    public GameObject playerBackpackGo;
    public GameObject playerBackpackFrmGo;

    private BackpackCtrl _backpackCtrl;
    private BackpackFrmCtrl _backpackFrmCtrl;
    private GameObject _backpackFrmGoInst;
    private RaycastHit _hit;

    private Camera _mainCamera;
    private Vector3 _mousePos;

    private void Start()
    {
      _mainCamera = Camera.main;
      if (_mainCamera == null) throw new Exception("No camera detected.");
      _backpackCtrl = playerBackpackGo.GetComponent<BackpackCtrl>();
      _backpackCtrl.Init();
      itemSpawnerCtrl.Init();
      itemSpawnerCtrl.Spawn(ItemKind.Lantern, playerGo.transform.position, null);
      // UI
      playerBackpackFrmGo.SetActive(false);
    }

    private void Update()
    {
      var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out _hit))
      {
        if (Input.GetMouseButtonDown(0)) OnMouseLeftBtnDown();
        if (Input.GetMouseButtonUp(0)) OnMouseLeftBtnUp();
      }
    }

    private void OnMouseLeftBtnDown()
    {
      if (!EventSystem.current.IsPointerOverGameObject())
        switch (_hit.collider.tag)
        {
          case "ResourceSource":
            itemSpawnerCtrl.Spawn(ItemKind.Ore, _hit.point, null);
            break;
        }
    }

    private void OnMouseLeftBtnUp()
    {
      var backpackItemSpawnPos = new Vector3(
        playerBackpackGo.transform.position.x - 0.7f,
        3f,
        playerBackpackGo.transform.position.z - 0.7f
      );
      if (_backpackFrmGoInst)
      {
        var hoveredItemAsset = _backpackFrmGoInst.GetComponent<BackpackFrmCtrl>().GetHoveredListItemAsset();
        if (hoveredItemAsset != null)
        {
          _backpackCtrl.RemoveItem(hoveredItemAsset.guid, hoveredItemAsset.kind);
          itemSpawnerCtrl.Spawn(
            hoveredItemAsset.kind,
            backpackItemSpawnPos,
            hoveredItemAsset
          );
        }
      }
      Destroy(_backpackFrmGoInst);
    }

    public void OpenBackpackFrm()
    {
      if (_backpackFrmGoInst) Destroy(_backpackFrmGoInst);
      _backpackFrmGoInst = Instantiate(playerBackpackFrmGo, uiGo.transform);
      _backpackFrmGoInst.GetComponent<BackpackFrmCtrl>().Init();
      _backpackFrmGoInst.SetActive(true);
    }
  }
}