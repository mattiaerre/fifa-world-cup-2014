using System;

namespace FWCB2014.Import.Core.Extensions
{
  public static class DoubleExtensions
  {
    public static DateTime ToUtc(this double epoch)
    {
      var dateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTimeUtc = dateTimeUtc.AddSeconds(epoch).ToUniversalTime();
      return dateTimeUtc;
    }
  }
}