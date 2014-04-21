using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Standings;
using FWCB2014.Domain.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FWCB2014.Syndication.Web.Controllers
{
  public class GroupsController : ApiController
  {
    private readonly IGroupsService<GroupModel<StandingModel>> _service;

    private readonly IDictionary<int, string> _mapping = new Dictionary<int, string>
    {
      {1, "Group A"},
      {2, "Group B"},
      {3, "Group C"},
      {4, "Group D"},
      {5, "Group E"},
      {6, "Group F"},
      {7, "Group G"},
      {8, "Group H"},
    };

    public GroupsController(IGroupsService<GroupModel<StandingModel>> service)
    {
      _service = service;
    }

    public IEnumerable<GroupModel<StandingModel>> Get()
    {
      var groups = _service.GetAll();
      return groups;
    }

    public IEnumerable<GroupModel<StandingModel>> Get(int id)
    {
      var groupName = _mapping.First(e => e.Key == id).Value;
      var groups = _service.Find(e => e.Name == groupName);
      return groups;
    }
  }
}
