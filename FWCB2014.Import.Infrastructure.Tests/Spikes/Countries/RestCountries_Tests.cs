using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

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
    public void List_Of_Countries()
    {
      var data = XElement.Load(@"C:\Users\mattia.richetto\Dropbox\dotNet\prj\FWCB2014\FWCB2014.Application.Web\App_Data\Standings_20140405.xml");

      var teams = data.Descendants("team").Select(e => new { Code = e.Attribute("id").Value, Name = e.Descendants("name").First().Value.ToLower() });

      var countries = new List<CountryModel>();

      foreach (var team in teams.Where(e => e.Name != "england"))
      {
        var country = GetCountry(team.Name);
        country.Code = team.Code;
        countries.Add(country);
      }

      countries.Add(new CountryModel
      {
        Code = "eng_int",
        Name = "England",
        Alpha2Code = "EN",
        Alpha3Code = "ENG",
      });

      var json = JsonConvert.SerializeObject(countries.OrderBy(e => e.Code));
    }

    private static CountryModel GetCountry(string countryName)
    {
      var uri = string.Format("http://restcountries.eu/rest/v1/name/{0}", countryName);

      //var uri = string.Format("http://restcountries.eu/rest/v1/alpha/{0}", countryAlpha3Code);
      var response = HttpHelper.HttpGet(uri);
      var country = JArray.Parse(response).First();

      var name = country.Children<JProperty>().First(e => e.Name == "name").Value.ToString();
      var alpha2Code = country.Children<JProperty>().First(e => e.Name == "alpha2Code").Value.ToString();
      var alpha3Code = country.Children<JProperty>().First(e => e.Name == "alpha3Code").Value.ToString();

      return new CountryModel { Name = name, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };
    }
  }
}
