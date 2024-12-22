using System;
using System.Collections.Generic;

namespace Project.Scripts.Entities.Backpack
{
  [Serializable]
  public class Backpack
  {
    public string guid;
    public string title;
    public string weightCapacity;

    public List<string> itemGuidList;
  }
}