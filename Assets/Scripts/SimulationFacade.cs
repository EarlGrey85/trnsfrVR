using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class Tasks
  {
    [JsonProperty("tasks")] public Dictionary<int, string> IdToTaskName { get; }
    [JsonProperty("currentTaskId")] public int CurrentTaskId { get; }
  }
  
  public class SimulationFacade : ITickable, IInitializable
  {
    private Http.Manager _httpManager;
    [JsonProperty] private Dictionary<string, string> _eventData;
    

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
      var res = Resources.Load<TextAsset>("FakeBackend/eventList").text;
      var data = JsonConvert.DeserializeObject<Tasks>(res);
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        _httpManager.StartAsyncRequest(string.Empty, _eventData, () => Debug.Log("success"), (a) => Debug.Log("fail" + a));
      }
    }

    void IInitializable.Initialize()
    {
      throw new System.NotImplementedException();
    }

    private void GetTaskList()
    {
      
    }
  }
}