using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Runner.Jobs
{
  public class CountriesJob : JobBase
  {
    public CountriesJob(ICountriesImportService service)
      : base(service, "countries and mapping")
    {
    }
  }
}
