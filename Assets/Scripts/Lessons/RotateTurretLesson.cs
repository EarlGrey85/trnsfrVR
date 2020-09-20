using UnityEngine;

namespace Simulation
{
  public class RotateTurretLesson : Lesson
  {
    private const float _goalAngle = 90;
    private readonly Transform _turretTransform = null;
    private float accumulatedAngle;
    private Quaternion prevRotation;
    
    public RotateTurretLesson(PlayerController playerController, string description) :
      base(playerController, description)
    {
      _turretTransform = playerController.TurretTransform;
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
  }
}