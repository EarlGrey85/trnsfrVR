using Simulation.UI;
using UnityEngine;
using Zenject;

public class TaskDescriptionInstaller : MonoInstaller
{
    [SerializeField] private TaskDescriptionViewModel.Installables _installables;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_installables);
        Container.BindInterfacesTo<TaskDescriptionViewModel>().AsSingle().NonLazy();
    }
}