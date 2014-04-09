using System.IO;
using System.Net;

namespace FWCB2014.Core.Infrastructure.Helpers
{
  public static class HttpHelper
  {
    public static string HttpGet(string uri)
    {
      var request = WebRequest.Create(uri);
      var response = request.GetResponse();
      var reader = new StreamReader(response.GetResponseStream());
      return reader.ReadToEnd().Trim();
    }
  }
}