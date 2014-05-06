using FWCB2014.Domain.Core.Models.Command.Matches;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Import.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Services
{
    [Obsolete("try to use a repository instead", false)]
    public class XmlMatchesService : IMatchesService<MatchModel>
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
                var model = new MatchModel
                {
                    CompetitionCode = match.Attribute("contest").Value,
                    SeasonCode = match.Descendants("season").First().Value,
                    Id = match.Attribute("id").Value,
                    Status = GetStatus(match.Attribute("status").Value),
                    Date = GetDate(match.Attribute("timestamp-starts").Value),
                    HomeTeam = GetTeam(match.Descendants("hosts").First().Attribute("id").Value),
                    AwayTeam = GetTeam(match.Descendants("guests").First().Attribute("id").Value),
                    FixtureInfo = match.Descendants("fixture-info").First().Value,
                    GroupId = GetGroupId(match.Descendants("group-id").FirstOrDefault()),
                };
                SetScore(match.Descendants("score").First().Value, model.HomeTeam, model.AwayTeam);
                yield return model;
            }
        }

        private string GetGroupId(XElement element)
        {
            if (element == null)
                return string.Empty;
            return element.Value;
        }

        private void SetScore(string value, TeamModel homeTeam, TeamModel awayTeam)
        {
            var score = value.Split('-');
            homeTeam.Score = Convert.ToInt16(score[0]);
            awayTeam.Score = Convert.ToInt16(score[1]);
        }

        private TeamModel GetTeam(string value)
        {
            return new TeamModel { Id = value, Score = 0 };
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