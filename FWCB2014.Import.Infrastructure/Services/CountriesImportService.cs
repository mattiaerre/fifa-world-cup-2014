using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Helpers;
using FWCB2014.Import.Core.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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

      IoHelper.SerializeAndSave(mapping.OrderBy(e => e.Key), _basePath, "Team_Country_Mapping", false);
      IoHelper.SerializeAndSave(countries.OrderBy(e => e.Name), _basePath, "Countries", false);
    }

    private CountryModel GetCountry(string countryName)
    {
      var uri = string.Format("{0}/name/{1}", _baseRestUri, countryName); // info: to me also name should be injected

      var response = HttpHelper.HttpGet(uri);
      var country = JArray.Parse(response).First();

      var name = country.Children<JProperty>().First(e => e.Name == "name").Value.ToString();
      var alpha2Code = country.Children<JProperty>().First(e => e.Name == "alpha2Code").Value.ToString();
      var alpha3Code = country.Children<JProperty>().First(e => e.Name == "alpha3Code").Value.ToString();

      return new CountryModel { Name = name, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };
    }
  }
}