using Http;
using Simulation;
using Zenject;

public class SimulationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SimulationFacade>().AsSingle().NonLazy();
        Container.Bind<Manager>().AsSingle();
        Container.BindFactory<Request, Request.Factory>().AsSingle();
    }
}