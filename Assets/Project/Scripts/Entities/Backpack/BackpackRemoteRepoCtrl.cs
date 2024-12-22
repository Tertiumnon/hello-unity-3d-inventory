using Project.Scripts.Core.RemoteRepo;
using UnityEngine;

namespace Project.Scripts.Entities.Backpack
{
  public class BackpackRemoteRepoCtrl : MonoBehaviour
  {
    private const string UrlPath = "inventory/status";
    
    public void CreateEvent(BackpackRemoteRepoEvent backpackRemoteRepoEvent)
    {
      StartCoroutine(RemoteRepo.Post(UrlPath, JsonUtility.ToJson(backpackRemoteRepoEvent)));
    }
  }
}