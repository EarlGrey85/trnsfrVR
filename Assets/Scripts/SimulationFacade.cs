using System.Collections.Generic;
using Http;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class SimulationData
  {
    [JsonProperty("tasks")] public string[] Tasks { get; }
    [JsonProperty("currentTaskId")] public int CurrentTaskId { get; }

    public SimulationData(string[] tasks, int currentTaskId)
    {
      Tasks = tasks;
      CurrentTaskId = currentTaskId;
    }
  }
  
  public class SimulationFacade : ITickable, IInitializable
  {
    private Http.Manager _httpManager;
    private Dictionary<string, string> _currentEventData = new Dictionary<string, string>();
    [JsonProperty] private SimulationData _simulationDataData;
    

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
    }

    void ITickable.Tick()
    {
    }

    private void ParseTasksData(Response response)
    {
      _simulationDataData = JsonConvert.DeserializeObject<SimulationData>(response.ResponseText);

      Debug.Log($"currentTaskId: {_simulationDataData.CurrentTaskId}");

      foreach (var task in _simulationDataData.Tasks)
      {
        Debug.Log(task);
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