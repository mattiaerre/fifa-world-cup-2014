using FWCB2014.Domain.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public class FileCountryRepository : CountryRepositoryBase
  {
    private readonly string _jsonPath;

    private IEnumerable<CountryModel> _countries;
    protected override IEnumerable<CountryModel> Countries
    {
      get
      {
        if (_countries == null)
        {
          var countriesJson = File.ReadAllText(_jsonPath);
          var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(countriesJson);
          _countries = countries;
        }
        return _countries;
      }
    }

    public FileCountryRepository(string jsonPath)
    {
      _jsonPath = jsonPath;
    }
  }
}
