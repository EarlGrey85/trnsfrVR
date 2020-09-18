using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Simulation
{
  public class SimulationFacade : ITickable
  {
    private Http.Manager _httpManager;
    private Dictionary<string, string> _eventData = new Dictionary<string, string>();

    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        _httpManager.StartAsyncRequest(string.Empty, _eventData, () => Debug.Log("success"), (a) => Debug.Log("fail" + a));
      }
    }
  }
}