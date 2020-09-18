using System.Collections.Generic;
using Http;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class Tasks
  {
    [JsonProperty("tasks")] public Dictionary<string, string> IdToTaskName { get; }
    [JsonProperty("currentTaskId")] public int CurrentTaskId { get; }

    public Tasks(Dictionary<string, string> idToTaskName, int currentTaskId)
    {
      IdToTaskName = idToTaskName;
      CurrentTaskId = currentTaskId;
    }
  }
  
  public class SimulationFacade : ITickable, IInitializable
  {
    private Http.Manager _httpManager;
    private Dictionary<string, string> _currentEventData = new Dictionary<string, string>();
    [JsonProperty] private Tasks _tasksData;
    

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        
      }
    }

    private void ParseTasksData(Response response)
    {
      _tasksData = JsonConvert.DeserializeObject<Tasks>(response.ResponseText);

      Debug.Log($"currentId: {_tasksData.CurrentTaskId}");

      foreach (var kv in _tasksData.IdToTaskName)
      {
        Debug.Log($"{kv.Key} : {kv.Value}");
      }
    }

    void IInitializable.Initialize()
    {
      GetTaskList();
    }

    private void GetTaskList()
    {
      _httpManager.StartAsyncRequest("getTasks", _currentEventData, ParseTasksData);
    }
  }
}