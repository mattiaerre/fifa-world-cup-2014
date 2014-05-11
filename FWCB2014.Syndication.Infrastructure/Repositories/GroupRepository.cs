using AutoMapper;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Syndication.Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Syndication.Infrastructure.Repositories
{
  public class GroupRepository : IGroupRepository
  {
    private readonly ICountryRepository _repository;
    private readonly string _teamCountryMappingJsonPath;
    private readonly string _standingsJsonPath;

    private IEnumerable<KeyValueModel> _teamCountryMapping;
    private IEnumerable<KeyValueModel> TeamCountryMapping
    {
      get
      {
        if (_teamCountryMapping == null)
        {
          var teamCountryMappingJson = File.ReadAllText(_teamCountryMappingJsonPath);
          var teamCountryMapping = JsonConvert.DeserializeObject<IEnumerable<KeyValueModel>>(teamCountryMappingJson);
          _teamCountryMapping = teamCountryMapping;
        }
        return _teamCountryMapping;
      }
    }

    private IEnumerable<StandingModel> _standings;
    private IEnumerable<StandingModel> Standings
    {
      get
      {
        if (_standings == null)
        {
          // todo: use azure store instead
          var standingsJson = File.ReadAllText(_standingsJsonPath);
          var standings = JsonConvert.DeserializeObject<IEnumerable<StandingModel>>(standingsJson);
          _standings = standings;
        }
        return _standings;
      }
    }

    public GroupRepository(ICountryRepository repository, string teamCountryMappingJsonPath, string standingsJsonPath)
    {
      _repository = repository;
      _teamCountryMappingJsonPath = teamCountryMappingJsonPath;
      _standingsJsonPath = standingsJsonPath;
    }

    public IEnumerable<GroupModel> Find(Func<GroupModel, bool> predicate)
    {
      var groups = Standings.GroupBy(e => e.GroupId);
      Mapper.CreateMap<CountryModel, TeamModel>();
      var list = new List<GroupModel>();
      foreach (var @group in groups)
      {
        var groupId = @group.First().GroupId;
        var model = new GroupModel { GroupId = groupId, Standings = GetStandings(@group), };
        list.Add(model);
      }
      return list.Where(predicate);
    }

    private IEnumerable<StandingModel> GetStandings(IEnumerable<StandingModel> standings)
    {
      var list = new List<StandingModel>();
      foreach (var standing in standings)
      {
        standing.Team = GetTeam(standing.TeamId);
        list.Add(standing);
      }
      return list;
    }

    private TeamModel GetTeam(string teamId)
    {
      var value = TeamCountryMapping.First(e => e.Key == teamId).Value;
      var team = Mapper.Map<TeamModel>(_repository.Find(e => e.Alpha3Code == value).First());
      team.TeamId = teamId;
      return team;
    }
  }
}