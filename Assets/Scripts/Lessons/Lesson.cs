using System;

namespace Simulation
{
  public abstract class Lesson
  {
    private bool _eventSent;
    private PlayerController _playerController = null;

    public static event Action<Lesson> Completed = delegate {  };
    public static event Action<Lesson> Started = delegate {  };

    public bool IsCompleted { get; private set; }
    public string Description { get; }

    protected abstract bool Perform();

    protected Lesson(PlayerController playerController, string description)
    {
      _playerController = playerController;
      Description = description;
    }
    
    public void Tick()
    {
      if (IsCompleted)
      {
        if (!_eventSent)
        {
          _eventSent = true;
          OnEnd();
        }
        
        return;
      }
      
      IsCompleted = Perform();
    }

    public virtual void OnStart()
    {
      Started.Invoke(this);
    }

    protected virtual void OnEnd()
    {
      Completed.Invoke(this);
    }
  }
}