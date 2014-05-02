using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Helpers;
using FWCB2014.Import.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FWCB2014.Import.Infrastructure.Services
{
    public class CountriesImportService : ICountriesImportService
    {
        private readonly Dictionary<string, string> _source;
        private readonly string _basePath;
        private readonly string _baseRestUri;

        public CountriesImportService(Dictionary<string, string> source, string basePath, string baseRestUri)
        {
            _source = source;
            _basePath = basePath;
            _baseRestUri = baseRestUri;
        }

        public void Import()
        {
            var mapping = new Dictionary<string, string>();
            var countries = new List<CountryModel>();

            foreach (var team in _source.Where(e => e.Value != "england"))
            {
                var country = GetCountry(team.Value);
                mapping.Add(team.Key, country.Alpha3Code);
                countries.Add(country);
            }

            const string englandName = "England";
            const string englandAlpha3Code = "ENG";
            mapping.Add("eng_int", englandAlpha3Code);
            countries.Add(new CountryModel
            {
                Name = englandName,
                Alpha2Code = englandName,
                Alpha3Code = englandAlpha3Code,
            });

            SerializeAndSave(mapping.OrderBy(e => e.Key), "Team_Country_Mapping", false);
            SerializeAndSave(countries.OrderBy(e => e.Name), "Countries", false);
        }

        private CountryModel GetCountry(string countryName)
        {
            var uri = string.Format("{0}/name/{1}", _baseRestUri, countryName);

            var response = HttpHelper.HttpGet(uri);
            var country = JArray.Parse(response).First();

            var name = country.Children<JProperty>().First(e => e.Name == "name").Value.ToString();
            var alpha2Code = country.Children<JProperty>().First(e => e.Name == "alpha2Code").Value.ToString();
            var alpha3Code = country.Children<JProperty>().First(e => e.Name == "alpha3Code").Value.ToString();

            return new CountryModel { Name = name, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };
        }

        private void SerializeAndSave(object data, string fileNamePrefix, bool withVersion)
        {
            var json = JsonConvert.SerializeObject(data);

            File.WriteAllText(string.Format(@"{0}\{1}{2}.json", _basePath, fileNamePrefix, withVersion ? string.Format("_{0}", DateTime.UtcNow.ToString("yyyyMMdd")) : string.Empty), json);
        }
    }
}