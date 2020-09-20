using System;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class Projectile : IPoolable<Transform, IMemoryPool>, IDisposable, ITickable
  {
    private IMemoryPool _pool = null;
    private readonly Transform _transform;
    private readonly TrailRenderer _trailRenderer;
    private readonly float _timeToLive;
    private readonly float _speed;
    private bool _isIdle;
    private float _liveTime;

    public Projectile(Transform transform, Settings _settings, TrailRenderer trailRenderer)
    {
      _transform = transform;
      _trailRenderer = trailRenderer;
      _timeToLive = _settings.TimeToLive;
      _speed = _settings.Speed;
    }

    public void Launch()
    {
      _isIdle = false;
      _liveTime = 0;
    }

    void ITickable.Tick()
    {
      if (_isIdle)
      {
        return;
      }

      if (_liveTime > _timeToLive)
      {
        Dispose();
      }

      _transform.position += _transform.forward * _speed * Time.deltaTime;
      _liveTime += Time.deltaTime;
    }

    public void OnDespawned()
    {
      _pool = null;
      _transform.gameObject.SetActive(false);
      _isIdle = true;
    }

    public void OnSpawned(Transform cannonEnd, IMemoryPool pool)
    {
      _pool = pool;
      _transform.position = cannonEnd.position;
      _transform.forward = cannonEnd.forward;
      _transform.gameObject.SetActive(true);
      _transform.parent = null;
      _trailRenderer.Clear();
    }

    public void Dispose()
    {
      _pool?.Despawn(this);
    }

    [Serializable]
    public class Settings
    {
      public float TimeToLive;
      public float Speed;
    }

    public class Factory : PlaceholderFactory<Transform, Projectile>
    {
    }
  }
}