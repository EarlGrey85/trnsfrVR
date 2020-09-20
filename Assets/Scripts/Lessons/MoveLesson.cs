using UnityEngine;

namespace Simulation
{
  public class MoveLesson : Lesson
  {
    private readonly Transform _playerTransform;
    private readonly float _goalDist;
    private float _currentDist;
    private Vector3 _prevPosition;

    public MoveLesson(PlayerController playerController, string description, MoveLessonSettings moveLessonSettings) 
      : base(playerController, description, moveLessonSettings)
    {
      _playerTransform = playerController.PlayerTransform;
      _goalDist = moveLessonSettings.GoalDistance;
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
    public class MoveLessonSettings : Settings
    {
      public float GoalDistance;
    }
  }
}