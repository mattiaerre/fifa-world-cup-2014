using System;
using FWCB2014.Domain.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  [Obsolete("every context will use its own implementation of the repositories", true)]
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

    public override void Add(string model)
    {
      throw new System.NotImplementedException();
    }

    public override void Delete()
    {
      throw new System.NotImplementedException();
    }

    public FileCountryRepository(string jsonPath)
    {
      _jsonPath = jsonPath;
    }
  }
}
