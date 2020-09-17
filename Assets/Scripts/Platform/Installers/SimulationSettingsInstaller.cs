using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SimulationSettingsInstaller", menuName = "Installers/SimulationSettingsInstaller")]
public class SimulationSettingsInstaller : ScriptableObjectInstaller<SimulationSettingsInstaller>
{
    [SerializeField] private Platform.Settings _settings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_settings);
    }
}