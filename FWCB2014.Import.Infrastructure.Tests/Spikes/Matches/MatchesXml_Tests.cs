using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Matches
{
  [TestFixture]
  public class MatchesXml_Tests : Base_Tests
  {
    private const string XmlPath = @"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Import.Infrastructure.Tests\Spikes\Matches\Matches_20140412.xml";

    private const int NumberOfMatches = 64;

    [SetUp]
    public void Given_An_API_Standings_Endpoint()
    {
    }

    [Test]
    [Ignore]
    public void I_Should_Be_Able_To_Get_All_The_Matches()
    {
      var url = string.Format("http://api.xmlscores.com/matches/?f=xml&c=wc_2014&open={0}&t1=1&l=128", ApiKey);
      var feed = HttpGet(url);

      File.WriteAllText(string.Format(@"C:\Users\mattiaerre\Source\Repos\fifa-world-cup-2014\FWCB2014.Import.Infrastructure.Tests\Spikes\Matches\Matches_{0}.xml", "20140412"), feed, Encoding.UTF8);

      Assert.IsNotNull(feed);
    }

    [Test]
    public void I_Should_Be_Able_To_Count_The_Number_Of_Matches()
    {
      var feed = XElement.Load(XmlPath);
      var items = feed.Descendants("matches").Descendants("item");
      Assert.AreEqual(NumberOfMatches, items.Count());
    }
  }
}