using System;
using FWCB2014.Domain.Core.Models.Command.Matches;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Import.Infrastructure.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Tests.Services
{
    [Obsolete("try to use a repository instead", false)]
    [TestFixture]
    [Explicit]
    public class XmlMatchesService_Tests
    {
        private IMatchesService<MatchModel> _service;

        [SetUp]
        public void Given_An_XmlMatchesService()
        {
            var feed = XElement.Load(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Matches_20140415.xml");
            _service = new XmlMatchesService(feed);
        }

        [Test]
        public void It_Should_Be_Able_To_Return_64_Matches()
        {
            var matches = _service.GetAll();

            var json = JsonConvert.SerializeObject(matches);

            File.WriteAllText(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Matches.json", json);

            Assert.AreEqual(64, matches.Count());
        }
    }
}