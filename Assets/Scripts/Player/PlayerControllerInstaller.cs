using Simulation;
using UnityEngine;
using Zenject;

public class PlayerControllerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController.Installables _installables = null;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.Bind<IWeapon>().FromSubContainerResolve().ByInstaller<WeaponInstaller>().AsSingle();
        Container.BindInstance(_installables);
    }
}