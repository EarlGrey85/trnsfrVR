using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Simulation.UI
{
  public class TaskDescriptionViewModel : ITickable, IInitializable
  {
    private readonly RectTransform _rectTransform;
    private readonly TextMeshProUGUI _textLabel;
    private readonly float _toggleDuration;

    private Vector2 _onScreenPosition;
    private Vector2 _outOfScreenPosition;
    private bool isVisible;
    
    public TaskDescriptionViewModel(Installables installables)
    {
      _rectTransform = installables.RT;
      _textLabel = installables.TextLabel;
      _toggleDuration = installables.ToggleDuration;
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
      _rectTransform.DOAnchorPos(show ? _onScreenPosition : _outOfScreenPosition, _toggleDuration);
    }

    void IInitializable.Initialize()
    {
      _rectTransform.anchoredPosition = Vector2.right * Screen.width;
      _onScreenPosition = Vector2.zero;
      _outOfScreenPosition = Vector2.right * Screen.width;
    }
    
    [System.Serializable]
    public class Installables
    {
      public RectTransform RT;
      public TextMeshProUGUI TextLabel;
      public float ToggleDuration = 0.25f;
    }
  }
}