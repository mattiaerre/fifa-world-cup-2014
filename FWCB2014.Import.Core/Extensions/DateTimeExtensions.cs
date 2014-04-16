using System;

namespace FWCB2014.Import.Core.Extensions
{
  public static class DateTimeExtensions
  {
    public static double ToEpoch(this DateTime dateTimeUtc)
    {
      return (dateTimeUtc - new DateTime(1970, 1, 1)).TotalSeconds;
    }
  }
}