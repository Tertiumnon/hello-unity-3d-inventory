using UnityEngine;

namespace Project.Scripts.Entities.Item
{
  [CreateAssetMenu(fileName = "ItemAsset", menuName = "Assets/Item")]
  public class ItemAsset : ScriptableObject
  {
    public string guid;
    public string title;
    public Sprite image;
    public string weight;
    public ItemKind kind;

    public GameObject prefab;
    public Material material;
  }
}