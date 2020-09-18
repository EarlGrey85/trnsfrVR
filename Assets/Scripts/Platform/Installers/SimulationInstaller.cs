using Http;
using Platform;
using Simulation;
using Zenject;

public class SimulationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SimulationFacade>().AsSingle().NonLazy();
        Container.Bind<Manager>().FromSubContainerResolve().ByInstaller<ManagerInstaller>().AsSingle();
        Container.Bind<FakePlatform>().AsSingle();
    }
}