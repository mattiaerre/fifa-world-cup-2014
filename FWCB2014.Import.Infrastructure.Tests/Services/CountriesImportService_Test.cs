using System.Collections.Generic;
using System.IO;
using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Infrastructure.Services;
using FWCB2014.Import.Infrastructure.Tests.Properties;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Services
{
    [TestFixture]
    public class CountriesImportService_Test
    {
        private ICountriesImportService _service;
        private readonly string _basePath = Settings.Default.SyndicationRoot;
        private const string AppDataFolder = "App_Data";

        [SetUp]
        public void Given_A_CountriesImportService()
        {
            var source = new Dictionary<string, string> { { "001", "brazil" } };

            const string baseRestUri = "http://restcountries.eu/rest/v1";

            _service = new CountriesImportService(source, string.Format(@"{0}\{1}", _basePath, AppDataFolder), baseRestUri);
        }

        [Test]
        public void It_Should_Be_Able_To_Import_One_Country()
        {
            _service.Import();

            var countriesPath = string.Format(@"{0}\{1}\Countries.json", _basePath, AppDataFolder);
            Assert.True(File.Exists(countriesPath));

            var mappingPath = string.Format(@"{0}\{1}\Team_Country_Mapping.json", _basePath, AppDataFolder);
            Assert.True(File.Exists(mappingPath));
        }
    }
}
