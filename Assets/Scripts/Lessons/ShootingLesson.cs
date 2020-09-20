namespace Simulation
{
  public class ShootingLesson : Lesson
  {
    private readonly int _requiredShotsNum;
    private int shotsCount;
    
    public ShootingLesson(PlayerController playerController, string description, ShootingLessonSettings settings) : 
      base(playerController, description, settings)
    {
      _requiredShotsNum = settings.ShotsNum;
    }

    ~ShootingLesson()
    {
      Weapon.ShotFired -= OnShotFired;
    }

    public override void OnStart()
    {
      base.OnStart();
      
      Weapon.ShotFired += OnShotFired;
    }

    private void OnShotFired()
    {
      ++shotsCount;
    }

    protected override bool Perform()
    {
      return shotsCount >= _requiredShotsNum;
    }

    [System.Serializable]
    public class ShootingLessonSettings : Settings
    {
      public int ShotsNum;
    }
  }
}