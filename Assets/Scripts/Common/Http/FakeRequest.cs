using System;
using System.Collections.Generic;
using UnityEngine;

namespace Http
{
  public class FakeRequest : IRequest
  {
    private readonly string _serverAddress;
    private readonly string _route;
    private readonly Action<Response> _successCallBack;
    private readonly Action<int> _failCallback;
    private readonly string _authenticationKey;
    private readonly Response.Factory _responseFactory;

    public FakeRequest(
      string serverAddress, 
      string route, 
      Action<Response> successCallBack, 
      Action<int> failCallback, 
      string authenticationKey, 
      Response.Factory responseFactory)
    {
      _serverAddress = "FakeBackend";
      _route = route;
      _successCallBack = successCallBack;
      _failCallback = failCallback;
      _authenticationKey = authenticationKey;
      _responseFactory = responseFactory;
    }
    
    public void Call() // it's a fake data provider. reading json from hard drive since I don't have real server
    {
      var responseJson = Resources.Load<TextAsset>($"{_serverAddress}/{_route}").text;
      var fakeHeaders = new Dictionary<string, string>();
      var response = _responseFactory.Create(fakeHeaders, responseJson);
      
      _successCallBack.Invoke(response);
    }

    public void AddData(string key, string value)
    {
    }

    public void AddData(Dictionary<string, string> data)
    {
    }
  }
}