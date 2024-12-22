using Project.Scripts.Components;
using Project.Scripts.Entities.Backpack;
using UnityEngine;

namespace Project.Scripts.Entities.Item
{
  [RequireComponent(typeof(DragAndDropCtrl))]
  public class ItemCtrl : MonoBehaviour
  {
    public ItemAsset asset;

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Backpack"))
      {
        var backpackGo = GameObject.Find("Backpack");
        backpackGo.GetComponent<BackpackCtrl>().AddItem(asset);
        Destroy(gameObject);
      }
    }
  }
}