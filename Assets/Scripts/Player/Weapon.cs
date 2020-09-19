using System;
using Zenject;

namespace Simulation
{
  public interface IWeapon
  {
    void Fire();
  }
  
  public class Weapon : IWeapon
  {
    public void Fire()
    {
      
    }
  }

  public class Projectile : IPoolable<IMemoryPool>, IDisposable
  {
    private IMemoryPool _pool = null;


    public void OnDespawned()
    {
      _pool = null;
    }

    void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
    {
      _pool = pool;
    }

    void IDisposable.Dispose()
    {
    }

    public class Factory : PlaceholderFactory<Projectile>
    {
    }
  }
}