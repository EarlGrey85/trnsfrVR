using UnityEngine;

namespace Simulation
{
  public class MoveLesson : Lesson
  {
    private readonly Transform _playerTransform;
    private const float _goalDist = 20;
    private float _currentDist;
    private Vector3 _prevPosition;
    
    public MoveLesson(PlayerController playerController) : base(playerController)
    {
      _playerTransform = playerController.PlayerTransform;
    }
    
    protected override bool Perform()
    {
      var position = _playerTransform.position;
      _currentDist += Mathf.Abs((position - _prevPosition).magnitude);
      _prevPosition = position;

      Debug.Log(_currentDist);

      return _currentDist > _goalDist;
    }

    public override void OnStart()
    {
      Debug.Log("go");
    }

    protected override void OnEnd()
    {
    }

    
  }
}