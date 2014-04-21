using FWCB2014.Domain.Core.Models.Query.Standings; // todo: it shoudn't be this one
using FWCB2014.Domain.Infrastructure.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Countries
{
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

      var countries = new List<TeamModel>();

      foreach (var team in teams.Where(e => e.Name != "england"))
      {
        var country = GetCountry(team.Name);
        country.Code = team.Code;
        countries.Add(country);
      }

      countries.Add(new TeamModel
      {
        Code = "eng_int",
        Name = "England",
        Alpha2Code = "EN",
        Alpha3Code = "ENG",
      });

      var json = JsonConvert.SerializeObject(countries.OrderBy(e => e.Code));

      File.WriteAllText(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Countries.json", json);
    }

    private static TeamModel GetCountry(string countryName)
    {
      var uri = string.Format("http://restcountries.eu/rest/v1/name/{0}", countryName);

      var response = HttpHelper.HttpGet(uri);
      var country = countryName == "iran" ? JArray.Parse(response).Skip(1).Take(1).First() : JArray.Parse(response).First();

      var name = country.Children<JProperty>().First(e => e.Name == "name").Value.ToString();
      var alpha2Code = country.Children<JProperty>().First(e => e.Name == "alpha2Code").Value.ToString();
      var alpha3Code = country.Children<JProperty>().First(e => e.Name == "alpha3Code").Value.ToString();

      return new TeamModel { Name = name, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };
    }
  }
}
