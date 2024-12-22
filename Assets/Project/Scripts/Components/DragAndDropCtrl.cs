using System;
using UnityEngine;

namespace Project.Scripts.Components
{
  public class DragAndDropCtrl : MonoBehaviour
  {
    private const float ItemHeight = 1.5f;
    public bool isDragging;
    private Camera _mainCamera;

    private GameObject _selectedItemGo;

    private void Start()
    {
      _mainCamera = Camera.main;
      if (_mainCamera == null) throw new Exception("Camera doesn't exist");
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        var hit = CastRay();
        if (_selectedItemGo == null)
          if (hit.collider != null)
            if (hit.collider.CompareTag("Resource") || hit.collider.CompareTag("Lantern"))
            {
              _selectedItemGo = hit.collider.gameObject;
              _selectedItemGo.GetComponent<Collider>().enabled = false;
              _selectedItemGo.GetComponent<Rigidbody>().isKinematic = true;
              Cursor.visible = false;
            }
      }
      if (_selectedItemGo != null)
      {
        if (isDragging)
        {
          var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            _mainCamera.WorldToScreenPoint(_selectedItemGo.transform.position).z);
          var worldPos = _mainCamera.ScreenToWorldPoint(pos);
          _selectedItemGo.transform.position = new Vector3(worldPos.x, ItemHeight, worldPos.z);
        }
        else
        {
          _selectedItemGo.GetComponent<Collider>().enabled = true;
          _selectedItemGo.GetComponent<Rigidbody>().isKinematic = false;
          var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            _mainCamera.WorldToScreenPoint(_selectedItemGo.transform.position).z);
          var worldPos = _mainCamera.ScreenToWorldPoint(pos);
          _selectedItemGo.transform.position = new Vector3(worldPos.x, ItemHeight, worldPos.z);
          _selectedItemGo = null;
          Cursor.visible = true;
        }
      }
    }

    private void OnMouseDown() { isDragging = true; }

    private void OnMouseUp() { isDragging = false; }

    private RaycastHit CastRay()
    {
      var screenFarMousePos = new Vector3(
        Input.mousePosition.x,
        Input.mousePosition.y,
        _mainCamera.farClipPlane
      );
      var screenNearMousePos = new Vector3(
        Input.mousePosition.x,
        Input.mousePosition.y,
        _mainCamera.nearClipPlane
      );
      var worldFarMousePos = _mainCamera.ScreenToWorldPoint(screenFarMousePos);
      var worldNearMousePos = _mainCamera.ScreenToWorldPoint(screenNearMousePos);
      Physics.Raycast(worldNearMousePos, worldFarMousePos - worldNearMousePos, out var hit);
      return hit;
    }
  }
}