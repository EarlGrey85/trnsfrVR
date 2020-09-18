using System;
using System.Collections.Generic;
using Zenject;

namespace Http
{
  public class ManagerInstaller : Installer
  {
    private Platform.Settings _platformSettings;
    
    public ManagerInstaller(Platform.Settings platformSettings)
    {
      _platformSettings = platformSettings;
    }
    
    public override void InstallBindings()
    {
      Container.Bind<Manager>().AsSingle();

      if (_platformSettings.IsDev)
      {
        Container.BindFactory<string, string, Action<Response>, Action<int>, string, IRequest, RequestFactory>().
          To<FakeRequest>();
      }
      else
      {
        Container.BindFactory<string, string, Action<Response>, Action<int>, string, IRequest, RequestFactory>().
          To<Request>();
      }

      Container.BindFactory<Dictionary<string, string>, string, Response, Response.Factory>().AsSingle();
    }
  }
}