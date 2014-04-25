using FWCB2014.Domain.Core.Models;
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
    private readonly Mock<IRepository<CountryModel>> _repository = new Mock<IRepository<CountryModel>>();

    [SetUp]
    public void Given_A_CountriesService()
    {
      _service = new CountriesService(_repository.Object);
    }

    [TestCase("BRA", "Brazil")]
    [TestCase("ITA", "Italy")]
    public void It_Should_Be_Able_To_Return_A_CountryModel_Given_A_CountryName(string countryCode, string countryName)
    {
      _repository.Setup(e => e.Find(c => c.Name == "WTF!")).Returns(new CountryModel { Name = countryName });

      var country = _service.GetCountry(countryCode);

      Assert.IsNotNull(country);
      Assert.AreEqual(countryCode, country.Code);
      Assert.AreEqual(countryName, country.Name);
    }
  }
}
