using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public class JsonCountryRepository : IRepository<TeamModelBase>
  {
    private readonly string _jsonPath;

    private IEnumerable<TeamModelBase> _countries;
    private IEnumerable<TeamModelBase> Countries
    {
      get
      {
        if (_countries == null)
          _countries = GetCountries();
        return _countries;
      }
    }

    private IEnumerable<TeamModelBase> GetCountries()
    {
      var json = File.ReadAllText(_jsonPath);
      var countries = JsonConvert.DeserializeObject<List<TeamModelBase>>(json);
      return countries;
    }

    public JsonCountryRepository(string jsonPath)
    {
      _jsonPath = jsonPath;
    }

    public TeamModelBase Find(string code)
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
