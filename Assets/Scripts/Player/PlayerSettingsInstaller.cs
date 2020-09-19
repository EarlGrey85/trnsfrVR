using Simulation;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
{
    [SerializeField] private PlayerController.CommonSettings _commonSettings = null;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_commonSettings);
    }
}