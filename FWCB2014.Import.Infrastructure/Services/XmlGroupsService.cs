using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FWCB2014.Domain.Core.Models.Groups;
using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Infrastructure.Services
{
  public class XmlGroupsService : IGroupsService
  {
    private readonly XElement _feed;

    public XmlGroupsService(XElement feed)
    {
      _feed = feed;
    }

    public IEnumerable<Group> GetAll()
    {
      var groups = _feed.Descendants("items").Descendants("item").GroupBy(e => e.Element("details").Value);
      foreach (var @group in groups)
      {
        yield return new Group { Name = GetName(@group.First().Element("details").Value), Teams = GetTeams(@group) };
      }
    }

    private static IEnumerable<Team> GetTeams(IEnumerable<XElement> @group)
    {
      foreach (var team in @group)
      {
        yield return new Team
        {
          Name = team.Element("team").Element("name").Value,
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
