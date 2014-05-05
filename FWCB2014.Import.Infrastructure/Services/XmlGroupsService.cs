using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Command.Standings;
using FWCB2014.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Services
{
  [Obsolete("Use the StandingsImportService instead", true)]
  public class XmlGroupsService : IGroupsService<GroupModel<StandingModel>>
  {
    private readonly XElement _feed;

    public XmlGroupsService(XElement feed)
    {
      _feed = feed;
    }

    public IEnumerable<GroupModel<StandingModel>> GetAll()
    {
      // todo: maybe I can get rid of the 1st "items" query part
      var groups = _feed.Descendants("items").Descendants("item").GroupBy(e => e.Element("details").Value);
      foreach (var @group in groups)
      {
        yield return new GroupModel<StandingModel> { Name = GetName(@group.First().Element("details").Value), Teams = GetTeams(@group) };
      }
    }

    public IEnumerable<GroupModel<StandingModel>> Find(Func<GroupModel<StandingModel>, bool> predicate)
    {
      throw new NotImplementedException();
    }

    private static IEnumerable<StandingModel> GetTeams(IEnumerable<XElement> @group)
    {
      foreach (var team in @group)
      {
        yield return new StandingModel
        {
          TeamId = team.Element("team").Attribute("id").Value,
          Position = Convert.ToInt32(team.Element("position").Value),
        };
      }
    }

    private static string GetName(string value)
    {
      return string.Format("Group {0}", value.Split('=')[1].Trim());
    }
  }
}
