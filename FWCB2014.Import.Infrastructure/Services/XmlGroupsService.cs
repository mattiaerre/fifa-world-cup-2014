using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Command.Groups;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Import.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Services
{
  public class XmlGroupsService : IGroupsService<GroupModel<TeamModel>>
  {
    private readonly XElement _feed;

    public XmlGroupsService(XElement feed)
    {
      _feed = feed;
    }

    public IEnumerable<GroupModel<TeamModel>> GetAll()
    {
      var groups = _feed.Descendants("items").Descendants("item").GroupBy(e => e.Element("details").Value);
      foreach (var @group in groups)
      {
        yield return new GroupModel<TeamModel> { Name = GetName(@group.First().Element("details").Value), Teams = GetTeams(@group) };
      }
    }

    private static IEnumerable<TeamModel> GetTeams(IEnumerable<XElement> @group)
    {
      foreach (var team in @group)
      {
        yield return new TeamModel
        {
          Code = team.Element("team").Attribute("id").Value,
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
