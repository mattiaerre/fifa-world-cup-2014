using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Core.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FWCB2014.Syndication.Infrastructure.Services
{
  public class JsonGroupsService : IGroupsService<GroupModel<TeamModel>>
  {
    private readonly string _jsonPath;
    private readonly IRepository<CountryModel> _repository;

    public JsonGroupsService(string jsonPath, IRepository<CountryModel> repository)
    {
      _jsonPath = jsonPath;
      _repository = repository;
    }

    public IEnumerable<GroupModel<TeamModel>> GetAll()
    {
      var json = File.ReadAllText(_jsonPath);
      var groups = JsonConvert.DeserializeObject<List<GroupModel<TeamModel>>>(json);

      foreach (var @group in groups)
      {
        foreach (var team in @group.Teams)
        {
          var country = _repository.Find(team.Code);
          team.Country = country;
        }
      }
      return groups;
    }
  }
}
