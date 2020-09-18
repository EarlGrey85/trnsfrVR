using System;
using Zenject;

namespace Http
{
  public class ManagerInstaller : Installer
  {
    public override void InstallBindings()
    {
      Container.Bind<Manager>().AsSingle();
      Container.BindFactory<string, Action, Action<int>, string, Request, Request.Factory>().AsSingle();
    }
  }
}