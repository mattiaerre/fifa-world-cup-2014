using System;
using FWCB2014.Domain.Core.Models.Command.Standings;
using FWCB2014.Domain.Infrastructure.Helpers;
using FWCB2014.Import.Core.Services;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Services
{
  public class StandingsImportService : IStandingsImportService
  {
    private readonly string _feedUri;
    private readonly string _basePath;

    public StandingsImportService(string feedUri, string basePath)
    {
      _feedUri = feedUri;
      _basePath = basePath;
    }

    public void Import()
    {
      // todo: separate the parts?
      // get
      var feed = HttpHelper.HttpGet(_feedUri);

      // transform
      var list = GetStandings(XElement.Parse(feed));
      var version = DateTime.Now.ToString("yyyyMMddHHmmssfff");
      var data = new { version = version, data = list };

      // save
      IoHelper.SerializeAndSave(data, _basePath, "Standings", false);
    }

    private static IEnumerable<StandingModel> GetStandings(XContainer feed)
    {
      var list = new List<StandingModel>();
      foreach (var item in feed.Descendants("item"))
      {
        list.Add(new StandingModel
        {
          TeamId = item.Element("team").Attribute("id").Value,
          GroupId = GetGroupId(item.Element("details").Value),
          Position = Convert.ToInt32(item.Element("position").Value),
          Played = Convert.ToInt32(item.Element("matches").Element("played").Value),
          Won = Convert.ToInt32(item.Element("matches").Element("won").Value),
          Drawn = Convert.ToInt32(item.Element("matches").Element("drawn").Value),
          Lost = Convert.ToInt32(item.Element("matches").Element("lost").Value),
          Scored = Convert.ToInt32(item.Element("goals").Element("scored").Value),
          Conceeded = Convert.ToInt32(item.Element("goals").Element("conceeded").Value),
          Points = Convert.ToInt32(item.Element("points").Value),
        });
      }
      return list;
    }

    private static string GetGroupId(string value)
    {
      return value.Split('=')[1].Trim();
    }
  }
}
