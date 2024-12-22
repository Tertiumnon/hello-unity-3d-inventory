using System;

namespace Project.Scripts.Entities.Item
{
  [Serializable]
  public class Item
  {
    public string guid;
    public string assetName;
  }

  public enum ItemKind
  {
    None,
    Ore,
    Pickaxe,
    Lantern
  }
}