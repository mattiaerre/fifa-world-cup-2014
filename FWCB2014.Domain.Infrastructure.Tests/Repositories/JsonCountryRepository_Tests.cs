using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Domain.Infrastructure.Tests.Properties;
using NUnit.Framework;

namespace FWCB2014.Domain.Infrastructure.Tests.Repositories
{
    [TestFixture]
    public class JsonCountryRepository_Tests
    {
        [TestCase("Italy")]
        public void It_Should_Return_A_Country_Given_Its_Code(string name)
        {
            var jsonPath = string.Format("{0}{1}", Settings.Default.SyndicationRoot, @"\App_Data\Countries.json");
            var repository = new JsonCountryRepository(jsonPath);

            var country = repository.Find(e => e.Name == name);

            Assert.IsNotNull(country);
        }
    }
}
