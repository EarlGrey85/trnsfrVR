using UnityEngine;
using Zenject;

namespace Simulation
{
  public class CameraController : ITickable
  {
    private readonly Transform _camRigTransform;
    private readonly Transform _camTransform;
    private readonly Transform _desiredPoint;
    private Vector3 moveVelocity;
    private Vector3 rotationVelocity;
    
    public CameraController(Installables installables, SimulationFacade.Installables simInstallables)
    {
      _camTransform = installables.CamTransform;
      _camRigTransform = installables.CamRigTransform;
      _desiredPoint = simInstallables.CameraDesiredPoint;
    }

    void ITickable.Tick()
    {
      var position = _camRigTransform.position;
      position = Vector3.SmoothDamp(position, _desiredPoint.position, ref moveVelocity, 0.2f);
      _camRigTransform.position = position;

      var forward = _camRigTransform.forward;
      forward = Vector3.SmoothDamp(forward, _desiredPoint.forward, ref rotationVelocity, 0.2f);
      _camRigTransform.forward = forward;
    }

    [System.Serializable]
    public class Installables
    {
      public Transform CamRigTransform;
      public Transform CamTransform;
    }
  }
}