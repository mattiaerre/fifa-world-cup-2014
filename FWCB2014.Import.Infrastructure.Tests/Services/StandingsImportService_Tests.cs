using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Infrastructure.Services;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Services
{
    [TestFixture]
    public class StandingsImportService_Tests : Base_Tests
    {
        private IStandingsImportService _service;

        [SetUp]
        public void Given_A_StandingsImportService()
        {
            var feedUri = string.Format("http://api.xmlscores.com/standings/?f=xml&c=wc_2014&open={0}", ApiKey);
            _service = new StandingsImportService(feedUri, SyndicationAppDataPath);
        }

        [Test]
        [Explicit]
        public void Do_Import()
        {
            _service.Import();
        }
    }
}