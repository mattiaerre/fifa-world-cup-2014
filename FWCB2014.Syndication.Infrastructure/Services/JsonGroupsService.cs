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
    private readonly IRepository<TeamModel> _repository;

    public JsonGroupsService(string jsonPath, IRepository<TeamModel> repository)
    {
      _jsonPath = jsonPath;
      _repository = repository;
    }

    public IEnumerable<GroupModel<StandingModel>> GetAll()
    {
      var json = File.ReadAllText(_jsonPath);
      var groups = JsonConvert.DeserializeObject<List<GroupModel<StandingModel>>>(json);

      foreach (var @group in groups)
      {
        foreach (var team in @group.Teams)
        {
          var country = _repository.Find(team.Code);
          team.Team = country;
        }
      }
      return groups.OrderBy(e => e.Name).ThenBy(e => e.Teams.OrderBy(t => t.Position));
    }
  }
}
