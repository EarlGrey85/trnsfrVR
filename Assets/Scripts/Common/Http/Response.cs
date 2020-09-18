using System.Collections.Generic;
using Zenject;

namespace Http
{
  public class Response
  {
    public string ResponseText { get; }
    public Dictionary<string, string> Headers { get; }

    public Response(Dictionary<string, string> headers, string responseText)
    {
      Headers = headers;
      ResponseText = responseText;
    }
    
    public class Factory : PlaceholderFactory<Dictionary<string, string>, string, Response>
    {
    }
  }
}