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
    protected abstract void OnStart();
    protected abstract void OnEnd();

    public Lesson(PlayerController playerController)
    {
      _playerController = playerController;
      
      OnStart();
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