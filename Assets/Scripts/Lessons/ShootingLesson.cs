namespace Simulation
{
  public class ShootingLesson : Lesson
  {
    private readonly int _requiredShotsNum;
    private int shotsCount;
    
    public ShootingLesson(PlayerController playerController, string description, Settings settings) : 
      base(playerController, description)
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
    public class Settings
    {
      public int ShotsNum;
    }
  }
}