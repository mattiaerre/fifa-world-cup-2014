using System.Web.Mvc;

namespace FWCB2014.Application.Web.Controllers
{
  public class GroupsController : Controller
  {
    public ActionResult Index(int? id)
    {
      ViewBag.GroupId = id;
      return View();
    }
  }
}