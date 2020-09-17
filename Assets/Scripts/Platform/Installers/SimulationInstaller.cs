using System;
using Http;
using Simulation;
using Zenject;

public class SimulationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SimulationFacade>().AsSingle().NonLazy();
        Container.Bind<Manager>().AsSingle();
        Container.BindFactory<string, Action, Action, Request, Request.Factory>().AsSingle();
    }
}