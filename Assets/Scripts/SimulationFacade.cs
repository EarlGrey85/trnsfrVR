using System.Collections.Generic;
using Http;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class SimulationData
  {
    [JsonProperty("tasks")] public SortedDictionary<string, string> Tasks { get; }
    [JsonProperty("currentTaskId")] public string CurrentTaskId { get; }

    public SimulationData(SortedDictionary<string, string> tasks, string currentTaskId)
    {
      Tasks = tasks;
      CurrentTaskId = currentTaskId;
    }
  }
  
  public class SimulationFacade : ITickable, IInitializable
  {
    private Http.Manager _httpManager;
    private Dictionary<string, string> _currentEventData = new Dictionary<string, string>();
    [JsonProperty] private SimulationData _simulationData;
    

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        GetNextTask();
      }
    }

    private void ParseTasksData(Response response)
    {
      _simulationData = JsonConvert.DeserializeObject<SimulationData>(response.ResponseText);

      Debug.Log($"currentTaskId: {_simulationData.CurrentTaskId}");

      foreach (var task in _simulationData.Tasks)
      {
        Debug.Log($"{task.Key} : {task.Value}");
      }
    }

    private void ParseNextTaskData(Response response)
    {
      var nextTask = response.ResponseText;
    }

    void IInitializable.Initialize()
    {
      GetTaskList();
      GetNextTask();
    }

    private void GetTaskList()
    {
      _httpManager.StartAsyncRequest("getTasks", _currentEventData, ParseTasksData);
    }

    private void GetNextTask()
    {
      _httpManager.StartAsyncRequest("nextTask", _currentEventData, ParseNextTaskData);
    }
  }
}