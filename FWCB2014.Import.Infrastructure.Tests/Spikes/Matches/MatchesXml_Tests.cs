using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FWCB2014.Import.Infrastructure.Tests.Properties;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Matches
{
    [TestFixture]
    public class MatchesXml_Tests : Base_Tests
    {
        private readonly string _xmlPath = string.Format("{0}{1}", Settings.Default.InfrastructureRoot, @"\Spikes\Matches\Matches_20140412.xml");

        private const int NumberOfMatches = 64;

        [SetUp]
        public void Given_An_API_Standings_Endpoint()
        {
        }

        [Test]
        [Explicit]
        public void I_Should_Be_Able_To_Get_All_The_Matches()
        {
            var url = string.Format("http://api.xmlscores.com/matches/?f=xml&c=wc_2014&open={0}&t1=1&l=128&e=1", ApiKey);
            var feed = HttpGet(url);

            File.WriteAllText(string.Format(@"{0}\Spikes\Matches\Matches_{1}.xml", Settings.Default.InfrastructureRoot, DateTime.Now.ToString("yyyyMMdd")), feed, Encoding.UTF8);

            Assert.IsNotNull(feed);
        }

        [Test]
        public void I_Should_Be_Able_To_Count_The_Number_Of_Matches()
        {
            var feed = XElement.Load(_xmlPath);
            var items = feed.Descendants("matches").Descendants("item");
            Assert.AreEqual(NumberOfMatches, items.Count());
        }
    }
}