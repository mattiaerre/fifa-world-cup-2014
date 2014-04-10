using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Syndication.Infrastructure.Services;
using System.Web.Mvc;

namespace FWCB2014.Application.Web.Controllers
{
  public class GroupsController : Controller
  {
    private IGroupsService<GroupModel<TeamModel>> _service;

    public GroupsController()
    {
    }

    public GroupsController(IGroupsService<GroupModel<TeamModel>> service)
    {
      _service = service;
    }

    public ActionResult Index()
    {
      _service = new JsonGroupsService(Server.MapPath(@"~/App_Data/Groups.json"), new JsonCountryRepository(Server.MapPath(@"~/App_Data/Countries.json")));
      var groups = _service.GetAll();
      return View(groups);
    }
  }
}