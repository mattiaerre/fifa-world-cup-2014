using System;
using AutoMapper;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Standings;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Core.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Syndication.Infrastructure.Services
{
  public class JsonGroupsService : IGroupsService<GroupModel<StandingModel>>
  {
    private readonly string _jsonPath;
    private readonly IRepository<TeamModelBase> _repository;

    public JsonGroupsService(string jsonPath, IRepository<TeamModelBase> repository)
    {
      _jsonPath = jsonPath;
      _repository = repository;
    }

    public IEnumerable<GroupModel<StandingModel>> GetAll()
    {
      return Find(e => true); // info: take all
    }

    public IEnumerable<GroupModel<StandingModel>> Find(Func<GroupModel<StandingModel>, bool> predicate)
    {
      var json = File.ReadAllText(_jsonPath);
      var groups = JsonConvert.DeserializeObject<List<GroupModel<StandingModel>>>(json)
        .Where(predicate).ToList();

      Mapper.CreateMap<TeamModelBase, TeamModel>();

      foreach (var @group in groups)
      {
        foreach (var team in @group.Teams)
        {
          // todo: check this
          var country = _repository.Find(team.Code);
          var destination = Mapper.Map<TeamModelBase, TeamModel>(country);
          team.Team = destination;
          // /todo: check this
        }
      }
      return groups.OrderBy(e => e.Name).ThenBy(e => e.Teams.OrderBy(t => t.Position));
    }
  }
}
