namespace Platform
{
  public class FakePlatform // here supposed to be some platform business logic. I'd put this on backend. 
  {
    public int CurrentTaskNum { get; private set; } = 1;

    public void IncrementCurrentTaskNum()
    {
      ++CurrentTaskNum;
    }
  }
}