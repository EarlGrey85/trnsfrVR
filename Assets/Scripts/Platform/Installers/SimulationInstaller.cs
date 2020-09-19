using Http;
using Platform;
using Simulation;
using UnityEngine;
using Zenject;

public class SimulationInstaller : MonoInstaller
{
    [SerializeField] private SimulationFacade.Installables _installables = null;
    [SerializeField] private Object _projectilePrefab;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SimulationFacade>().AsSingle().NonLazy();
        Container.Bind<Manager>().FromSubContainerResolve().ByInstaller<ManagerInstaller>().AsSingle();
        Container.Bind<FakePlatform>().AsSingle();
        Container.BindInstance(_projectilePrefab);
        Container.BindInstance(_installables);
    }
}