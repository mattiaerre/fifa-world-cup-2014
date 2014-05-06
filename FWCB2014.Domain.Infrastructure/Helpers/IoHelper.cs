using System;
using System.IO;
using Newtonsoft.Json;

namespace FWCB2014.Domain.Infrastructure.Helpers
{
  public static class IoHelper
  {
    public static void SerializeAndSave(object data, string basePath, string fileNamePrefix, bool withVersion)
    {
      var json = JsonConvert.SerializeObject(data, Formatting.Indented); // todo: add as parameter

      File.WriteAllText(string.Format(@"{0}\{1}{2}.json",
        basePath,
        fileNamePrefix,
        withVersion ? string.Format("_{0}", DateTime.UtcNow.ToString("yyyyMMdd")) : string.Empty), json);
    }
  }
}
