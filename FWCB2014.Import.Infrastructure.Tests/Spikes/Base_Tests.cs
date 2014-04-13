using System.IO;
using System.Net;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes
{
  public abstract class Base_Tests
  {
    protected const string ApiKey = "6b38225a69b2277dc7ecc512e70b66ad";

    public static string HttpGet(string uri)
    {
      var request = WebRequest.Create(uri);
      var response = request.GetResponse();
      var reader = new StreamReader(response.GetResponseStream());
      return reader.ReadToEnd().Trim();
    }
  }
}
