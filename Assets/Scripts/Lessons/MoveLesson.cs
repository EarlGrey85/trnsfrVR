using UnityEngine;

namespace Simulation
{
  public class MoveLesson : Lesson
  {
    public MoveLesson(PlayerController playerController) : base(playerController)
    {
    }
    
    protected override bool Perform()
    {
      Debug.Log("suka");

      return false;
    }

    protected override void OnStart()
    {
      Debug.Log("qeqeq");
    }

    protected override void OnEnd()
    {
    }

    
  }
}