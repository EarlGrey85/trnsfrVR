using System;
using Zenject;

namespace Simulation
{
  public interface ILesson
  {
    
  }
  
  public abstract class Lesson : ILesson
  {
    private readonly string _id = null;
    private readonly Settings _settings;
    private bool _eventSent;
    private PlayerController _playerController = null;

    public static event Action<Lesson> Completed = delegate {  };
    public static event Action<Lesson> Started = delegate {  };

    public bool IsCompleted { get; private set; }
    public string Description { get; } = null;

    protected abstract bool Perform();

    protected Lesson(PlayerController playerController, string description, Settings settings)
    {
      _playerController = playerController;
      Description = description;
      _settings = settings;
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

    [Serializable]
    public abstract class Settings
    {
      
    }
  }
}