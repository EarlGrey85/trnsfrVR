using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Http
{
  public class Request
  {
    private Action _successCallback;
    private Action _failCallback;
    private readonly string _url;
    private readonly WebHeaderCollection _headers;
    private readonly WebRequest _request;
    private Dictionary<string, string> _data = new Dictionary<string, string>();
    
    public Request(string url, Action successCallBack, Action failCallback)
    {
      _url = url;
      _successCallback = successCallBack;
      _failCallback = failCallback;
      _headers = new WebHeaderCollection
      {
        {"signature", Manager.SIGNATURE},
        {"token", Manager.SessionToken}
      };

      _request = WebRequest.CreateHttp(_url);
      _request.ContentType = "application/x-www-form-urlencoded";
      _request.Headers = _headers;
      _request.Method = "POST";
    }

    public void AddData(string key, string value)
    {
      if (_data.ContainsKey(key))
      {
        Debug.LogWarning($"key {key} already exists in request's payload");
        return;
      }

      _data[key] = value;
    }

    private byte[] GetDataArray()
    {
      var memStream = new MemoryStream();
      var binFormatter = new BinaryFormatter();
      binFormatter.Serialize(memStream, _data);
      return memStream.ToArray();
    }
    
    public async void Call()
    {
      try
      {
        var stream = await _request.GetRequestStreamAsync();
        var dataArray = GetDataArray();
        _request.ContentLength = dataArray.Length;
        
        await stream.WriteAsync(dataArray, 0, dataArray.Length);

        var response = await _request.GetResponseAsync();
        
        using (var dataStream = response.GetResponseStream())
        {
          var reader = new StreamReader(dataStream ?? throw new Exception("dataStream is null"));
          var responseFromServer = await reader.ReadToEndAsync();
          Debug.LogWarning(((HttpWebResponse) response).StatusCode);
          
          _successCallback.Invoke();
        }
      }
      catch (WebException ex)
      {
        using (var stream = ex.Response.GetResponseStream())
        using (var reader = new StreamReader(stream ?? throw new Exception("stream is null")))
        {
          var errorMsg = await reader.ReadToEndAsync();
          Debug.LogError($"{ex}\n{errorMsg}");
          
          _failCallback.Invoke();
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"it's either networking problem or something even worse: {ex}");
      }
    }

    public class Factory : PlaceholderFactory<string, Action, Action, Request>
    {
    }
  }
}