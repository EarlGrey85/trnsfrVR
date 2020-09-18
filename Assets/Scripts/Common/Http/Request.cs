using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Http
{
  public class Request
  {
    private Action _successCallback;
    private Action<int> _failCallback;
    private readonly string _url;
    private readonly WWWForm _wwwForm;
    private readonly UnityWebRequest _request;
    private readonly string _authenticationKey;

    public Request(string url, Action successCallBack, Action<int> failCallback, string authenticationKey)
    {
      _url = url;
      _successCallback = successCallBack;
      _failCallback = failCallback;
      _authenticationKey = authenticationKey;
      _wwwForm = new WWWForm();
      
      _wwwForm.headers["token"] = Manager.SessionToken;
      _wwwForm.headers["signature"] = "application/x-www-form-urlencoded";

      _request = UnityWebRequest.Post(_url, _wwwForm);
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

      const int statusCode = (int) ResponseCode.NoError;
      if (_request.responseCode != statusCode)
      {
        Debug.LogError(_request.responseCode);
        _failCallback.Invoke(statusCode);
      }
      
      Debug.Log(_request.responseCode);
      _successCallback.Invoke();
    }
    
    private static string ComputeRequestHash (string salt, byte[] data) 
    {
      return Manager.ComputeHash (salt + Encoding.UTF8.GetString (data, 0, data.Length));
    }

    public class Factory : PlaceholderFactory<string, Action, Action<int>, string, Request>
    {
    }
  }
}