using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Platform;
using Simulation;
using UnityEngine;
using Zenject;

namespace Http
{
  public class FakeRequest : IRequest, IInitializable
  {
    private readonly string _serverAddress;
    private readonly string _route;
    private readonly Action<Response> _successCallBack;
    private readonly Action<int> _failCallback;
    private readonly string _authenticationKey;
    private readonly Response.Factory _responseFactory;
    private readonly Dictionary<string, string> _fakeHeaders;
    private readonly Response _getTasksFakeResponse;
    private readonly SimulationData _simulationData;
    private readonly FakePlatform _fakePlatform;

    public FakeRequest(
      string serverAddress, 
      string route, 
      Action<Response> successCallBack, 
      Action<int> failCallback, 
      string authenticationKey, 
      Response.Factory responseFactory,
      FakePlatform fakePlatform)
    {
      _serverAddress = "FakeBackend";
      _route = route;
      _successCallBack = successCallBack;
      _failCallback = failCallback;
      _authenticationKey = authenticationKey;
      _responseFactory = responseFactory;
      _fakePlatform = fakePlatform;
      
      var responseJson = Resources.Load<TextAsset>($"{_serverAddress}/tasksList").text;
      _simulationData = JsonConvert.DeserializeObject<SimulationData>(responseJson);
      _fakeHeaders = new Dictionary<string, string>();
      _getTasksFakeResponse = _responseFactory.Create(_fakeHeaders, responseJson);
    }
    
    public void Call() // it's a fake data provider. reading json from hard drive since I don't have real server
    {
      var response = GetFakeResponse(_route);
      _successCallBack.Invoke(response);
    }

    private Response GetFakeResponse(string route)
    {
      switch (route)
      {
        case "getTasks":
          return _getTasksFakeResponse;
        case "nextTask":

          var currentTaskId = $"task{_fakePlatform.CurrentTaskNum}";

          if (!_simulationData.TaskDescriptionMap.ContainsKey(currentTaskId))
          {
            Debug.LogWarning($"no more tasks");
            currentTaskId = string.Empty;
          }

          Debug.Log(currentTaskId);
          ++_fakePlatform.CurrentTaskNum;
          
          var nextTaskResponse = _responseFactory.Create(_fakeHeaders, currentTaskId);
          return nextTaskResponse;
      }

      return null;
    }

    public void AddData(string key, string value)
    {
    }

    public void AddData(Dictionary<string, string> data)
    {
    }

    public void Initialize()
    {
      Debug.Log("VAR");
    }
  }
}