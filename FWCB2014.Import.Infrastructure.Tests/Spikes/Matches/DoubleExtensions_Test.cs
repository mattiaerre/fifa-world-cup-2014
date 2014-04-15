using System.Globalization;
using NUnit.Framework;
using System;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Matches
{
  [TestFixture]
  public class DoubleExtensions_Test
  {
    [TestCase("1402603200", 2014, 6, 12, 20, 0, 0)]
    [TestCase("1402675200", 2014, 6, 13, 16, 0, 0)]
    [TestCase("1402686000", 2014, 6, 13, 19, 0, 0)]
    public void It_Should_Return_An_Utc_Date_And_Converting_It_Back_Into_An_Epoch(string epoch, int year, int month, int day, int hour, int minute, int second)
    {
      var dateTimeUtc = Convert.ToDouble(epoch).ToUtc();

      Assert.AreEqual(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc), dateTimeUtc);
      Assert.AreEqual(epoch, dateTimeUtc.ToEpoch().ToString(CultureInfo.InvariantCulture));
    }
  }

  public static class DoubleExtensions
  {
    public static DateTime ToUtc(this double epoch)
    {
      var dateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      dateTimeUtc = dateTimeUtc.AddSeconds(epoch).ToUniversalTime();
      return dateTimeUtc;
    }
  }
  public static class DateTimeExtensions
  {
    public static double ToEpoch(this DateTime dateTimeUtc)
    {
      return (dateTimeUtc - new DateTime(1970, 1, 1)).TotalSeconds;
    }
  }
}
