using FWCB2014.Import.Infrastructure.Tests.Properties;

namespace FWCB2014.Import.Infrastructure.Tests
{
  public abstract class Base_Tests
  {
    protected const string ApiKey = "6b38225a69b2277dc7ecc512e70b66ad";
    protected string SyndicationAppDataPath = string.Format(@"{0}\{1}", Settings.Default.SyndicationRoot, "App_Data");
  }
}
