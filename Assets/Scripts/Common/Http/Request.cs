using System;
using System.IO;
using System.Net;
using UnityEngine;
using Zenject;

namespace Http
{
  public class Request
  {
    public Request(string url, Action successCallBack, Action failCallback)
    {
      Call(url, successCallBack, failCallback);
    }
    
    private static async void Call(string url, Action successCallBack, Action failCallback)
    {
      var request = WebRequest.Create(url);
      var response = await request.GetResponseAsync();

      using (var dataStream = response.GetResponseStream())
      {
        var reader = new StreamReader(dataStream ?? throw new Exception("dataStream is null"));
        var responseFromServer = await reader.ReadToEndAsync();
        Debug.Log(responseFromServer);
      }
    }

    public class Factory : PlaceholderFactory<string, Action, Action, Request>
    {
    }
  }
}