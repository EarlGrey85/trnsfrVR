using UnityEngine;
using Zenject;

namespace Simulation
{
  public class WeaponInstaller : Installer
  {
    [Inject] private Object projectilePrefab;
    
    public override void InstallBindings()
    {
      Container.Bind<IWeapon>().To<Weapon>().AsSingle();
      Container.BindFactory<Projectile, Projectile.Factory>().FromPoolableMemoryPool<Projectile>
        (x => x.WithInitialSize(5).FromSubContainerResolve().ByNewContextPrefab(projectilePrefab));
    }
  }
}