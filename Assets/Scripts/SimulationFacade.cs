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
      
    }
  }
}