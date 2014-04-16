using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public class JsonCountryRepository : IRepository<TeamModel>
  {
    private readonly string _jsonPath;

    private IEnumerable<TeamModel> _countries;
    private IEnumerable<TeamModel> Countries
    {
      get
      {
        if (_countries == null)
          _countries = GetCountries();
        return _countries;
      }
    }

    private IEnumerable<TeamModel> GetCountries()
    {
      var json = File.ReadAllText(_jsonPath);
      var countries = JsonConvert.DeserializeObject<List<TeamModel>>(json);
      return countries;
    }

    public JsonCountryRepository(string jsonPath)
    {
      _jsonPath = jsonPath;
    }

    public TeamModel Find(string code)
    {
      var country = Countries.First(e => e.Code == code);
      // hack
      if (country.Name == "England")
        country.Alpha2Code = "England";
      // /hack
      return country;
    }
  }
}
