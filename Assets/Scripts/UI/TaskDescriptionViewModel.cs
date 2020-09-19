using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Simulation.UI
{
  public class TaskDescriptionViewModel : ITickable, IInitializable, IDisposable
  {
    private readonly RectTransform _rectTransform;
    private readonly TextMeshProUGUI _textLabel;
    private readonly float _toggleDuration;
    private readonly float _delay;

    private Vector2 _onScreenPosition;
    private Vector2 _outOfScreenPosition;
    private bool isVisible;

    public TaskDescriptionViewModel(Installables installables)
    {
      _rectTransform = installables.RT;
      _textLabel = installables.TextLabel;
      _toggleDuration = installables.ToggleDuration;
      _delay = installables.Delay;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.Z))
      {
        isVisible = !isVisible;
        Toggle(isVisible);
      }
    }

    private void Toggle(bool show)
    {
      if (show)
      {
        var showUpSequence = DOTween.Sequence();
        showUpSequence.Append(_rectTransform.DOAnchorPos(_onScreenPosition, _toggleDuration));
        showUpSequence.PrependInterval(_delay);
      }
      else
      {
        _rectTransform.DOAnchorPos(_outOfScreenPosition, _toggleDuration);
      }
    }

    void IInitializable.Initialize()
    {
      _rectTransform.anchoredPosition = Vector2.right * Screen.width;
      _onScreenPosition = Vector2.zero;
      _outOfScreenPosition = Vector2.right * Screen.width;
      
      Lesson.Started += OnStarted;
      Lesson.Completed += OnLessonCompleted;
      SimulationFacade.TutorialCompleted += OnTutorialCompleted;
    }
    
    void IDisposable.Dispose()
    {
      Lesson.Started -= OnStarted;
      Lesson.Completed -= OnLessonCompleted;
      SimulationFacade.TutorialCompleted -= OnTutorialCompleted;
    }

    private void OnTutorialCompleted()
    {
      _textLabel.text = "Tutorial completed! Please finish the exercise.";
      Toggle(true);
    }

    private void OnStarted(Lesson lesson)
    {
      _textLabel.text = lesson.Description;
      Toggle(true);
    }

    private void OnLessonCompleted(Lesson lesson)
    {
      Toggle(false);
    }
    
    [System.Serializable]
    public class Installables
    {
      public RectTransform RT;
      public TextMeshProUGUI TextLabel;
      public float ToggleDuration = 0.25f;
      public float Delay = 1;
    }
  }
}