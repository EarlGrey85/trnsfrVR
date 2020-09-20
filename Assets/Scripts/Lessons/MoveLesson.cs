using UnityEngine;

namespace Simulation
{
  public class MoveLesson : Lesson
  {
    private readonly Transform _playerTransform;
    private readonly float _goalDist;
    private float _currentDist;
    private Vector3 _prevPosition;

    public MoveLesson(PlayerController playerController, string description, Settings settings) 
      : base(playerController, description)
    {
      _playerTransform = playerController.PlayerTransform;
      _goalDist = settings.GoalDistance;
    }

    public override void OnStart()
    {
      base.OnStart();
      
      _prevPosition = _playerTransform.position;
    }

    protected override bool Perform()
    {
      var position = _playerTransform.position;
      _currentDist += Mathf.Abs((position - _prevPosition).magnitude);
      _prevPosition = position;

      return _currentDist > _goalDist;
    }

    [System.Serializable]
    public class Settings
    {
      public float GoalDistance;
    }
  }
}