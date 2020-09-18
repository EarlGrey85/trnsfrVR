using System.Collections.Generic;

namespace Http
{
  public interface IRequest
  {
    void Call();
    void AddData(string key, string value);
    void AddData(Dictionary<string, string> data);
  }
}