using System;
using System.Linq;
using System.Xml.Linq;
using FWCB2014.Domain.Core.Models.Command.Matches;
using FWCB2014.Domain.Core.Services;
using System.Collections.Generic;
using FWCB2014.Import.Core.Extensions;

namespace FWCB2014.Import.Infrastructure.Services
{
  public class XmlMatchesService : IMatchesService
  {
    private readonly IEnumerable<KeyValuePair<string, MatchStatus>> _mapping = new List<KeyValuePair<string, MatchStatus>>
    {
      new KeyValuePair<string, MatchStatus>("not_started", MatchStatus.NotStarted),
    };
    private readonly XElement _feed;

    public XmlMatchesService(XElement feed)
    {
      _feed = feed;
    }

    public IEnumerable<MatchModel> GetAll()
    {
      var matches = _feed.Descendants("item");
      foreach (var match in matches)
      {
        yield return new MatchModel
        {
          CompetitionCode = match.Attribute("contest").Value,
          Code = match.Attribute("id").Value,
          Status = GetStatus(match.Attribute("status").Value),
          Date = GetDate(match.Attribute("timestamp-starts").Value),
        };
      }
    }

    private DateTime GetDate(string value)
    {
      return Convert.ToDouble(value).ToUtc();
    }

    private MatchStatus GetStatus(string value)
    {
      return _mapping.First(e => e.Key == value).Value;
    }
  }
}