using FWCB2014.Domain.Infrastructure.Repositories;
using NUnit.Framework;

namespace FWCB2014.Domain.Infrastructure.Tests.Repositories
{
  [TestFixture]
  public class JsonCountryRepository_Tests
  {
    [TestCase("ita_int")]
    public void It_Should_Return_A_Country_Given_Its_Code(string countryCode)
    {
      const string jsonPath = @"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Countries.json";
      var repository = new JsonCountryRepository(jsonPath);

      var country = repository.Find(e => e.Name == "WTF!");

      Assert.IsNotNull(country);
    }
  }
}
