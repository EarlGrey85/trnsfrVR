using UnityEngine;

namespace Simulation
{
  public class RotateTurretLesson : Lesson
  {
    private readonly float _goalAngle;
    private readonly Transform _turretTransform = null;
    private float accumulatedAngle;
    private Quaternion prevRotation;
    
    public RotateTurretLesson(PlayerController playerController, string description, RotateLessonSettings rotateLessonSettings) :
      base(playerController, description, rotateLessonSettings)
    {
      _turretTransform = playerController.TurretTransform;
      _goalAngle = rotateLessonSettings.GoalAngle;
    }

    public override void OnStart()
    {
      base.OnStart();
      
      prevRotation = _turretTransform.localRotation;
    }

    protected override bool Perform()
    {
      var rotation = _turretTransform.localRotation;
      accumulatedAngle += Quaternion.Angle(rotation, prevRotation);
      prevRotation = rotation;

      return accumulatedAngle > _goalAngle;
    }

    [System.Serializable]
    public class RotateLessonSettings : Settings
    {
      public float GoalAngle;
    }
  }
}