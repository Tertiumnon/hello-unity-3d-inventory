using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Scripts.Core.RemoteRepo
{
  public static class RemoteRepo
  {
    private const string Server = "https://wadahub.manerai.com/api";
    private const string BearerToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";
      
    public static IEnumerator Post(string urlPath, string postData)
    {
      using var webRequest = UnityWebRequest.Post(
        $"{Server}/{urlPath}", 
        postData, 
        "application/json"
        );
      webRequest.SetRequestHeader("Authorization", $"Bearer {BearerToken}");
      yield return webRequest.SendWebRequest();
      if (webRequest.result != UnityWebRequest.Result.Success)
      {
        Debug.LogError(webRequest.error);
      }
      else
      {
        Debug.Log("Request complete!");
      }
    }
  }
}