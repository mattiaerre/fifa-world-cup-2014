using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
    public class CountryRepository : IRepository<CountryModel>
    {
        private readonly string _jsonPath;

        private IEnumerable<CountryModel> _countries;
        private IEnumerable<CountryModel> Countries
        {
            get
            {
                if (_countries == null)
                    _countries = GetCountries();
                return _countries;
            }
        }

        private IEnumerable<CountryModel> GetCountries()
        {
            var json = File.ReadAllText(_jsonPath);
            var countries = JsonConvert.DeserializeObject<List<CountryModel>>(json);
            return countries;
        }

        public CountryRepository(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public IEnumerable<CountryModel> Find(Func<CountryModel, bool> predicate)
        {
            var countries = Countries.Where(predicate);
            return countries;
        }
    }
}
