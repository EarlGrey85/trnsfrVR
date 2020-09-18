using UnityEngine;
using Zenject;

namespace Simulation
{
  public class SimulationFacade : ITickable
  {
    private Http.Manager _httpManager;
    
    public SimulationFacade(Http.Manager httpManager)
    {
      _httpManager = httpManager;
    }

    void ITickable.Tick()
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        _httpManager.StartAsyncRequest(string.Empty, () => Debug.Log("success"), () => Debug.Log("fail"));
      }
    }
  }
}