using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace FWCB2014.Syndication.Web.Controllers
{
  public class GroupsController : ApiController
  {
    private readonly IGroupsService<GroupModel<TeamModel>> _service;
    
    public GroupsController(IGroupsService<GroupModel<TeamModel>> service)
    {
      _service = service;
    }

    public IEnumerable<GroupModel<TeamModel>> Get()
    {
      var groups = _service.GetAll();
      return groups;
    }
  }
}
