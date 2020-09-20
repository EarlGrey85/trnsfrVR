using Simulation;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectileSettingsInstaller", menuName = "Installers/ProjectileSettingsInstaller")]
public class ProjectileSettingsInstaller : ScriptableObjectInstaller<ProjectileSettingsInstaller>
{
    [SerializeField] private Projectile.Settings _settings = null;
    public override void InstallBindings()
    {
        Container.BindInstance(_settings);
    }
}