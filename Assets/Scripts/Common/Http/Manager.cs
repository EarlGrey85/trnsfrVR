using System;
using UnityEngine;

namespace Http
{
  public enum Error
  {
    NoError = 0,
    BadRequest = 400,
    NotFound = 404,
    Unoauthorized = 401,
    ServerError = 500,
  }
  
  public class Manager
  {
    public const string SIGNATURE = "SOME_SIGNATURE";
    
    public const string ROUTE_LOGIN = "/transfr/platform/login";
    public const string ROUTE_LOGOUT = "/transfr/platform/logout";
    public const string ROUTE_EVENTS = "/transfr/platform/events";
    
    private Platform.Settings _platformSettings;
    private Request.Factory _requestFactory;
    private string _url;

    public static string SessionToken { get; }

    public Manager(Platform.Settings platformSettings, Request.Factory factory)
    {
      _platformSettings = platformSettings;
      _url = $"{platformSettings.Protocol}://{platformSettings.Domain}";
      _requestFactory = factory;
      
      Debug.Log(_url);
    }

    public void StartAsyncRequest(string route, Action successCallback, Action failCallback)
    {
      Debug.Log($"{_url}/{route}");
      var request = _requestFactory.Create($"{_url}/{route}", successCallback, failCallback);
      request.Call();
    }
  }
}