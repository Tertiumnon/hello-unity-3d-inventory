using System.Collections.Generic;
using Project.Scripts.Entities.Item;
using UnityEngine;

namespace Project.Scripts.Entities.Backpack
{
  [CreateAssetMenu(fileName = "BackpackAsset", menuName = "Assets/Backpack")]
  public class BackpackAsset : ScriptableObject
  {
    public string guid;
    public string title;
    public string weightCapacity;

    public ItemAsset lanternItemAsset;

    public List<ItemAsset> itemAssetList;
  }
}