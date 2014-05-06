using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FWCB2014.Domain.Infrastructure.Helpers;
using FWCB2014.Import.Infrastructure.Tests.Properties;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Standings
{
    [TestFixture]
    public class StandingsXml_Tests : Base_Tests
    {
        private readonly string _xmlPath = string.Format("{0}{1}", Settings.Default.InfrastructureRoot, @"\Spikes\Standings\Standings_20140412.xml");

        private const int NumberOfTeams = 32;
        private const int NumberOfGroups = 8;

        [SetUp]
        public void Given_An_API_Standings_Endpoint()
        {
        }

        [Test]
        [Explicit]
        public void I_Should_Be_Able_To_Get_All_The_Standings()
        {
            var url = string.Format("http://api.xmlscores.com/standings/?f=xml&c=wc_2014&open={0}", ApiKey);
            var feed = HttpHelper.HttpGet(url);

            File.WriteAllText(string.Format(@"{0}\Spikes\Standings\Standings_{1}.xml", Settings.Default.InfrastructureRoot, DateTime.Now.ToString("yyyyMMdd")), feed, Encoding.UTF8);

            Assert.IsNotNull(feed);
        }

        [Test]
        public void I_Should_Be_Able_To_Count_The_Number_Of_Teams()
        {
            var feed = XElement.Load(_xmlPath);
            var items = feed.Descendants("items").Descendants("item");
            Assert.AreEqual(NumberOfTeams, items.Count());
        }

        [Test]
        public void I_Should_Be_Able_To_Count_The_Number_Of_Groups()
        {
            var feed = XElement.Load(_xmlPath);
            var items = feed.Descendants("items").Descendants("item").GroupBy(e => e.Element("details").Value);
            Assert.AreEqual(NumberOfGroups, items.Count());
        }
    }
}
