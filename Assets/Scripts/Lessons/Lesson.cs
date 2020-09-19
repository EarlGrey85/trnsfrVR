using System;
using Zenject;

namespace Simulation
{
  public abstract class Lesson : ITickable
  {
    private readonly string _id = null;
    private bool _eventSent;
    
    public static event Action<string> LessonCompleted = delegate {  };
    
    public bool IsCompleted { get; private set; }

    protected abstract bool Perform();
    public abstract void OnStart();
    public abstract void OnEnd();
    
    public void Tick()
    {
      if (IsCompleted)
      {
        if (!_eventSent)
        {
          LessonCompleted.Invoke(_id);
          _eventSent = true;
        }
        
        return;
      }
      
      IsCompleted = Perform();
    }
  }
}