using Zenject;

namespace Simulation
{
  public class ProjectileInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<Projectile>().AsSingle();
    }
  }
}