using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Http
{
  public class Request : IRequest
  {
    private Action<Response> _successCallback;
    private Action<int> _failCallback;
    private readonly string _serverAddress;
    private readonly string _route;
    private readonly WWWForm _wwwForm;
    private readonly UnityWebRequest _request;
    private readonly string _authenticationKey;
    private readonly Response.Factory _responseFactory;

    public Request(
      string serverAddress, 
      string route, 
      Action<Response> successCallBack, 
      Action<int> failCallback, 
      string authenticationKey, 
      Response.Factory responseFactory)
    {
      _serverAddress = serverAddress;
      _route = route;
      _successCallback = successCallBack;
      _failCallback = failCallback;
      _authenticationKey = authenticationKey;
      _responseFactory = responseFactory;
      _wwwForm = new WWWForm();
      
      _wwwForm.headers["token"] = Manager.SessionToken;
      _wwwForm.headers["signature"] = "application/x-www-form-urlencoded";

      _request = UnityWebRequest.Post($"{serverAddress}/{route}", _wwwForm);
    }

    public void AddData(string key, string value)
    {
      _wwwForm.AddField(key, value);
    }

    public void AddData(Dictionary<string, string> data)
    {
      foreach (var kv in data)
      {
        _wwwForm.AddField(kv.Key, kv.Value);
      }
    }

    public async void Call()
    {
      _wwwForm.headers[Manager.SIGNATURE_HEADER] = ComputeRequestHash(_authenticationKey, _wwwForm.data);
      await _request.SendWebRequest();

      if (_request.responseCode != (int) ResponseCode.NoError)
      {
        _failCallback.Invoke((int) _request.responseCode);
        return;
      }

      Debug.Log(_request.responseCode);
      var response = _responseFactory.Create(_request.GetResponseHeaders(), _request.downloadHandler.text);
      _successCallback.Invoke(response);
    }
    
    private static string ComputeRequestHash (string salt, byte[] data) 
    {
      return Manager.ComputeHash (salt + Encoding.UTF8.GetString (data, 0, data.Length));
    }
  }
  
  public class RequestFactory : PlaceholderFactory<string, string, Action<Response>, Action<int>, string, IRequest>
  {
  }
}