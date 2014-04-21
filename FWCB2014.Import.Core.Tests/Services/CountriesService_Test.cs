using FWCB2014.Domain.Core.Models.Query.Standings; // todo: it shoudn't be this one
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Import.Core.Services;
using Moq;
using NUnit.Framework;

namespace FWCB2014.Import.Core.Tests.Services
{
  [TestFixture]
  public class CountriesService_Test
  {
    private ICountriesService _service;
    private readonly Mock<IRepository<TeamModel>> _repository = new Mock<IRepository<TeamModel>>();

    [SetUp]
    public void Given_A_CountriesService()
    {
      _service = new CountriesService(_repository.Object);
    }

    [TestCase("BRA", "Brazil")]
    [TestCase("ITA", "Italy")]
    public void It_Should_Be_Able_To_Return_A_CountryModel_Given_A_CountryName(string countryCode, string countryName)
    {
      _repository.Setup(e => e.Find(countryCode)).Returns(new TeamModel { Code = countryCode, Name = countryName});

      var country = _service.GetCountry(countryCode);

      Assert.IsNotNull(country);
      Assert.AreEqual(countryCode, country.Code);
      Assert.AreEqual(countryName, country.Name);
    }
  }
}
