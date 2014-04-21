using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Import.Core.Services
{
  public class CountriesService : ICountriesService
  {
    private readonly IRepository<TeamModelBase> _repository;

    public CountriesService(IRepository<TeamModelBase> repository)
    {
      _repository = repository;
    }

    public TeamModelBase GetCountry(string countryCode)
    {
      return _repository.Find(countryCode);
    }
  }
}