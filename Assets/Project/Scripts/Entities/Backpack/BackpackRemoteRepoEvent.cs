using System;
using Project.Scripts.Entities.Item;

namespace Project.Scripts.Entities.Backpack
{
  [Serializable]
  public class BackpackRemoteRepoEvent
  {
    public ItemEventKind eventKind;
    public string itemGuid;
    public ItemKind itemKind;
  }
  
  public enum ItemEventKind
  {
    None,
    AddToBackpack,
    RemoveFromBackpack
  }
}