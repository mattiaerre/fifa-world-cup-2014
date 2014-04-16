using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Import.Core.Services
{
  public class CountriesService : ICountriesService
  {
    private readonly IRepository<TeamModel> _repository;

    public CountriesService(IRepository<TeamModel> repository)
    {
      _repository = repository;
    }

    public TeamModel GetCountry(string countryCode)
    {
      return _repository.Find(countryCode);
    }
  }
}