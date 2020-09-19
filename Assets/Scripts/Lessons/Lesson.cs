using System;

namespace Simulation
{
  public abstract class Lesson
  {
    private readonly string _id = null;
    private bool _eventSent;
    private PlayerController _playerController;
    
    public static event Action<string> LessonCompleted = delegate {  };
    
    public bool IsCompleted { get; private set; }

    protected abstract bool Perform();
    public abstract void OnStart();
    protected abstract void OnEnd();

    protected Lesson(PlayerController playerController)
    {
      _playerController = playerController;
    }
    
    public void Tick()
    {
      if (IsCompleted)
      {
        if (!_eventSent)
        {
          LessonCompleted.Invoke(_id);
          _eventSent = true;
          OnEnd();
        }
        
        return;
      }
      
      IsCompleted = Perform();
    }
  }
}