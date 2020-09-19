using UnityEngine;
using Zenject;

namespace Simulation
{
  public class CameraControllerInstaller : MonoInstaller
  {
    [SerializeField] private CameraController.Installables _installables = null;

    public override void InstallBindings()
    {
      Container.BindInstance(_installables);
      Container.BindInterfacesTo<CameraController>().AsSingle().NonLazy();
    }
  }
}