﻿using System;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class PlayerController : ITickable, IInitializable
  {
    private readonly Transform _vehicleTransform;
    private readonly Transform _turretTransform;
    private readonly float _speed;
    private readonly float _rotationSpeed;
    private readonly float _turretRotationSpeed;
    private readonly IWeapon _weapon;

    public Transform PlayerTransform => _vehicleTransform;
    public Transform TurretTransform => _turretTransform;
    
    public static event Action<PlayerController> PlayerReady = delegate {  };

    public PlayerController(CommonSettings commonSettings, Installables installables, IWeapon weapon)
    {
      _vehicleTransform = installables.VehicleTransform;
      _turretTransform = installables.TurretTransform;
      _speed = commonSettings.Speed;
      _rotationSpeed = commonSettings.RotationSpeed;
      _turretRotationSpeed = commonSettings.TurretRotationSpeed;
      _weapon = weapon;

      Debug.LogError(weapon);
    }

    void IInitializable.Initialize()
    {
      PlayerReady.Invoke(this);
    }
    
    public void Tick()
    {
      var inputY = Input.GetAxis("Vertical");
      var inputX = Input.GetAxis("Horizontal");
      var turretControl = Input.GetAxis("Turret");

      _vehicleTransform.position += _vehicleTransform.forward * _speed * inputY * Time.deltaTime;
      _vehicleTransform.rotation *= Quaternion.AngleAxis(_rotationSpeed * inputX * Time.deltaTime, _vehicleTransform.up);
      _turretTransform.rotation *= Quaternion.AngleAxis(_turretRotationSpeed * turretControl * Time.deltaTime, _turretTransform.up);
    }

    [System.Serializable]
    public class Installables
    {
      public Transform VehicleTransform;
      public Transform TurretTransform;
    }
    
    [System.Serializable]
    public class CommonSettings
    {
      public float Speed;
      public float RotationSpeed;
      public float TurretRotationSpeed;
    }
  }
}