﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class SimulationData
  {
    [JsonProperty("tasks")] public SortedDictionary<string, string> TaskDescriptionMap { get; }
    [JsonProperty("currentTaskId")] public string CurrentTaskId { get; }

    public SimulationData(SortedDictionary<string, string> taskDescriptionMap, string currentTaskId)
    {
      TaskDescriptionMap = taskDescriptionMap;
      CurrentTaskId = currentTaskId;
    }
  }
  
  public class SimulationFacade : ITickable, IInitializable, IDisposable
  {
    private readonly MoveLesson.Settings _moveLessonSettings;
    private readonly RotateTurretLesson.Settings _rotateLessonSettings;
    private readonly ShootingLesson.Settings _shootingLessonSettings;
    
    private readonly Http.Manager _httpManager;
    private Dictionary<string, string> _currentEventData = new Dictionary<string, string>();
    private Lesson _currentLesson;
    private Dictionary<string, Lesson> _lessonsMap = new Dictionary<string, Lesson>();
    private string currentTaskId;
    [JsonProperty] private SimulationData _simulationData;
    
    public static event Action TutorialCompleted = delegate {  };
    
    public SimulationFacade(
      Http.Manager httpManager,
      MoveLesson.Settings moveLessonSettings, 
      RotateTurretLesson.Settings rotateLessonSettings,
      ShootingLesson.Settings shootingLessonSettings)
    {
      _httpManager = httpManager;
      _moveLessonSettings = moveLessonSettings;
      _rotateLessonSettings = rotateLessonSettings;
      _shootingLessonSettings = shootingLessonSettings;
    }
    
    void IInitializable.Initialize()
    {
      GetTaskList();

      PlayerController.PlayerReady += OnPlayerReady;
      Lesson.Completed += OnLessonCompleted;
    }

    void IDisposable.Dispose()
    {
      PlayerController.PlayerReady -= OnPlayerReady;
      Lesson.Completed -= OnLessonCompleted;
    }

    void ITickable.Tick()
    {
      _currentLesson.Tick();
    }

    private void OnPlayerReady(PlayerController playerController)
    {
      _lessonsMap = new Dictionary<string, Lesson>
      {
        {"task1", new MoveLesson(playerController, GetTaskDescription("task1"), _moveLessonSettings)},
        {"task2", new RotateTurretLesson(playerController, GetTaskDescription("task2"), _rotateLessonSettings)},
        {"task3", new ShootingLesson(playerController, GetTaskDescription("task3"), _shootingLessonSettings)},
      };
      
      _httpManager.StartAsyncRequest("nextTask", _currentEventData, ProcessNextTaskData);
    }

    private string GetTaskDescription(string taskId)
    {
      if (!_simulationData.TaskDescriptionMap.TryGetValue(taskId, out var description))
      {
        return string.Empty;
      }

      return description;
    }

    private void ParseTasksData(Http.Response response)
    {
      _simulationData = JsonConvert.DeserializeObject<SimulationData>(response.ResponseText);
    }

    private void ProcessNextTaskData(Http.Response response)
    {
      currentTaskId = response.ResponseText;

      if (!_lessonsMap.TryGetValue(currentTaskId, out var nextLesson))
      {
        TutorialCompleted.Invoke();
        Debug.Log($"tutorial completed!");
        return;
      }

      if (!_simulationData.TaskDescriptionMap.TryGetValue(currentTaskId, out var description))
      {
        Debug.LogWarning($"no description for taskId: {currentTaskId}");
      }

      Debug.Log(_simulationData.TaskDescriptionMap[currentTaskId]);
      _currentLesson = nextLesson;
      _currentLesson.OnStart();
    }
    
    private void GetTaskList()
    {
      _httpManager.StartAsyncRequest("getTasks", _currentEventData, ParseTasksData);
    }

    private void OnLessonCompleted(Lesson lesson)
    {
      _currentEventData.Clear();
      _currentEventData["taskId"] = currentTaskId;
      _currentEventData["eventType"] = string.Empty; // I don't have that info
      _currentEventData["timeStamp"] = ((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
      //_currentEventData["objectState"] = some serialized state, don't know exactly what info you need here
      //_currentEventData["stepId"] , stepId is the same as taskId in my case. 
      _httpManager.StartAsyncRequest("nextTask", _currentEventData, ProcessNextTaskData);
    }

    [System.Serializable]
    public class Installables
    {
      public Transform CameraDesiredPoint;
    }
  }
}