using FWCB2014.Domain.Infrastructure.Repositories;
using NUnit.Framework;

namespace FWCB2014.Domain.Infrastructure.Tests.Repositories
{
  [TestFixture]
  public class JsonCountryRepository_Tests
  {
    [TestCase("ITA")]
    public void It_Should_Return_A_Country_Given_Its_Code(string countryCode)
    {
      const string jsonPath = @"C:\Users\mattia.richetto\Dropbox\dotNet\prj\FWCB2014\FWCB2014.Application.Web\App_Data\Countries.json";
      var repository = new JsonCountryRepository(jsonPath);

      var country = repository.Find(countryCode);

      Assert.IsNotNull(country);
    }
  }
}
