using UnityEngine;
using Zenject;

namespace Simulation
{
  public class ProjectileInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesAndSelfTo<Projectile>().AsSingle();
      Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
      Container.Bind<TrailRenderer>().FromComponentOnRoot().AsSingle();
    }
  }
}