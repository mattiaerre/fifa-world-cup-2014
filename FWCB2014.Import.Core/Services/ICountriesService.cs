using FWCB2014.Domain.Core.Models;

namespace FWCB2014.Import.Core.Services
{
  public interface ICountriesService
  {
    TeamModelBase GetCountry(string countryCode);
  }
}