using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Syndication.Infrastructure.Repositories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FWCB2014.Syndication.Infrastructure.Tests.Repositories
{
  [TestFixture]
  [Explicit]
  public class GroupRepository_Tests
  {
    private IFind<GroupModel> _repository;

    [SetUp]
    public void Given_A_GroupRepository()
    {
      var countriesJsonPath = @"D:\Users\mattia\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Countries.json";
      var teamCountryMappingJsonPath = @"D:\Users\mattia\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Team_Country_Mapping.json";
      var standingsJsonPath = @"D:\Users\mattia\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Standings.json";

      _repository = new GroupRepository(null, teamCountryMappingJsonPath, standingsJsonPath);
    }

    [Test]
    public void It_Should_Find()
    {
      var list = _repository.Find(e => true);

      var json = JsonConvert.SerializeObject(list);
    }
  }
}