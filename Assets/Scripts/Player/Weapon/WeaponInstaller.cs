using UnityEngine;
using Zenject;

namespace Simulation
{
  public class WeaponInstaller : Installer
  {
    private readonly GameObject _projectilePrefab;

    public WeaponInstaller(PlayerController.Installables installables)
    {
      _projectilePrefab = installables.ProjectilePrefab;
    }
    
    public override void InstallBindings()
    {
      Container.Bind<IWeapon>().To<Weapon>().AsSingle();
      Container.BindFactory<Transform, Projectile, Projectile.Factory>().FromPoolableMemoryPool
        (x => x.WithInitialSize(0).FromSubContainerResolve().ByNewContextPrefab(_projectilePrefab));
    }
  }
}