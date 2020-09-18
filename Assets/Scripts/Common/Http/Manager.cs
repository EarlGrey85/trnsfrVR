using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Http
{
  public enum ResponseCode
  {
    NoError = 200,
    BadRequest = 400,
    NotFound = 404,
    Unoauthorized = 401,
    ServerError = 500,
  }
  
  public class Manager
  {
    public const string SIGNATURE_HEADER = "SOME_SIGNATURE";
    
    public const string ROUTE_LOGIN = "/transfr/platform/login";
    public const string ROUTE_LOGOUT = "/transfr/platform/logout";
    public const string ROUTE_EVENTS = "/transfr/platform/events";
    
    private Platform.Settings _platformSettings;
    private RequestFactory _requestFactory;
    private string _serverAddress;

    public static string SessionToken { get; private set; }

    public Manager(Platform.Settings platformSettings, RequestFactory factory)
    {
      _platformSettings = platformSettings;
      _serverAddress = $"{platformSettings.Protocol}://{platformSettings.Domain}";
      _requestFactory = factory;
      
      Debug.Log(_serverAddress);
    }

    public void StartAsyncRequest(
      string route, 
      Dictionary<string, string> data, 
      Action<Response> successCallback = null, 
      Action<int> failCallback = null)
    {
      var request = _requestFactory.Create(
        _serverAddress,
        route,
        successCallback, 
        failCallback, 
        _platformSettings.AuthenticationKey);

      request.AddData(data);
      request.Call();
    }
    
    public static string ComputeHash (string data)
    {
      var hash = new System.Security.Cryptography.SHA512Managed ();
      var ba = hash.ComputeHash (Encoding.UTF8.GetBytes (data));
      var sb = new StringBuilder (ba.Length * 2);
      
      foreach (var b in ba) 
      {
        sb.AppendFormat ("{0:x2}", b);
      }
      return sb.ToString ();
    }
  }
}