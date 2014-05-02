using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Countries
{
  // todo: add this class to the import structure
  [TestFixture]
  public class RestCountries_Tests
  {
    [TestCase("Brazil", "BR", "BRA")]
    [TestCase("Italy", "IT", "ITA")]
    public void Get_Country_Info(string countryName, string countryAlpha2Code, string countryAlpha3Code)
    {
      var country = GetCountry(countryAlpha3Code);

      Assert.IsNotNull(country);

      Assert.AreEqual(countryName, country.Name);
      Assert.AreEqual(countryAlpha2Code, country.Alpha2Code);
      Assert.AreEqual(countryAlpha3Code, country.Alpha3Code);
    }

    [Test]
    [Explicit]
    public void List_Of_Countries()
    {
      var data = XElement.Load(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Standings_20140405.xml");

      var teams = data.Descendants("team").Select(e => new { Code = e.Attribute("id").Value, Name = e.Descendants("name").First().Value.ToLower() });

      var mapping = new Dictionary<string, string>();
      var countries = new List<CountryModel>();

      foreach (var team in teams.Where(e => e.Name != "england"))
      {
        var country = GetCountry(team.Name);
        mapping.Add(team.Code, country.Alpha3Code);
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

    private static void SerializeAndSave(object data, string fileNamePrefix, bool withVersion)
    {
      var json = JsonConvert.SerializeObject(data);

      File.WriteAllText(string.Format(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\{0}{1}.json",
        fileNamePrefix,
        withVersion? string.Format("_{0}", DateTime.UtcNow.ToString("yyyyMMdd")) : string.Empty),
        json);
    }

    private static CountryModel GetCountry(string countryName)
    {
      var uri = string.Format("http://restcountries.eu/rest/v1/name/{0}", countryName);

      var response = HttpHelper.HttpGet(uri);
      var country = JArray.Parse(response).First();

      var name = country.Children<JProperty>().First(e => e.Name == "name").Value.ToString();
      var alpha2Code = country.Children<JProperty>().First(e => e.Name == "alpha2Code").Value.ToString();
      var alpha3Code = country.Children<JProperty>().First(e => e.Name == "alpha3Code").Value.ToString();

      return new CountryModel { Name = name, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };
    }
  }
}
