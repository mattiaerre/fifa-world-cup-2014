using System.Web.Mvc;
using System.Xml.Linq;
using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Infrastructure.Services;

namespace FWCB2014.Application.Web.Controllers
{
  public class GroupsController : Controller
  {
    private IGroupsService _service;

    public GroupsController()
    {
    }

    public GroupsController(IGroupsService service) // todo: add container
    {
      _service = service;
    }

    public ActionResult Index()
    {
      _service = new XmlGroupsService(XElement.Load(Server.MapPath(@"~/App_Data/Standings_20140405.xml")));

      var groups = _service.GetAll();
      return View(groups);
    }
  }
}