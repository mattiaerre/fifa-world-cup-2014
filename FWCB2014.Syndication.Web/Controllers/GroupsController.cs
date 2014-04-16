using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Services;
using System.Collections.Generic;
using System.Web.Http;
using StandingModel = FWCB2014.Domain.Core.Models.Query.Standings.StandingModel;

namespace FWCB2014.Syndication.Web.Controllers
{
  public class GroupsController : ApiController
  {
    private readonly IGroupsService<GroupModel<StandingModel>> _service;
    
    public GroupsController(IGroupsService<GroupModel<StandingModel>> service)
    {
      _service = service;
    }

    public IEnumerable<GroupModel<StandingModel>> Get()
    {
      var groups = _service.GetAll();
      return groups;
    }
  }
}
