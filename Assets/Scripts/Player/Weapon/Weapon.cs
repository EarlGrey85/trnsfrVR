using System;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public interface IWeapon
  {
    void Fire();
  }
  
  public class Weapon : IWeapon
  {
    private readonly Projectile.Factory _projectileFactory = null;
    private readonly Transform _cannonEndTransform = null;

    public Weapon(Projectile.Factory projectileFactory, PlayerController.Installables installables)
    {
      _projectileFactory = projectileFactory;
      _cannonEndTransform = installables.CannonEndTransform;
    }
    
    public void Fire()
    {
      var projectile = _projectileFactory.Create(_cannonEndTransform);
      projectile.Launch();
    }
  }
}