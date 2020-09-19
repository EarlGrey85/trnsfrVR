using System.Collections.Generic;
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
    private PlayerController _playerController;
    private Http.Manager _httpManager;
    private Dictionary<string, string> _currentEventData = new Dictionary<string, string>();
    [JsonProperty] private SimulationData _simulationData;

    private Lesson _currentLesson;
    private Lesson _moveLesson;

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
      
    }
    
    void IInitializable.Initialize()
    {
      GetTaskList();
      GetNextTask();

      PlayerController.PlayerReady += OnPlayerReady;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        GetNextTask();
      }
      
      _currentLesson.Tick();
    }

    private void OnPlayerReady(PlayerController playerController)
    {
      _playerController = playerController;
      _moveLesson = new MoveLesson(playerController);
      _currentLesson = _moveLesson;
      _currentLesson.OnStart();
    }

    private void ParseTasksData(Http.Response response)
    {
      _simulationData = JsonConvert.DeserializeObject<SimulationData>(response.ResponseText);

      Debug.Log($"currentTaskId: {_simulationData.CurrentTaskId}");

      foreach (var task in _simulationData.Tasks)
      {
        Debug.Log($"{task.Key} : {task.Value}");
      }
    }

    private void ParseNextTaskData(Http.Response response)
    {
      var nextTask = response.ResponseText;
    }

    

    private void GetTaskList()
    {
      _httpManager.StartAsyncRequest("getTasks", _currentEventData, ParseTasksData);
    }

    private void GetNextTask()
    {
      _httpManager.StartAsyncRequest("nextTask", _currentEventData, ParseNextTaskData);
    }

    [System.Serializable]
    public class Installables
    {
      public Transform CameraDesiredPoint;
    }
  }
}