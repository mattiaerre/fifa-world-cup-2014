using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Services;
using System.Web.Mvc;

namespace FWCB2014.Application.Web.Controllers
{
  public class GroupsController : Controller
  {
    private readonly IGroupsService<GroupModel<TeamModel>> _service;

    public GroupsController(IGroupsService<GroupModel<TeamModel>> service)
    {
      _service = service;
    }

    public ActionResult Index()
    {
      var groups = _service.GetAll();
      return View(groups);
    }
  }
}