using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Syndication.Infrastructure.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace FWCB2014.Syndication.Web.Controllers
{
  public class GroupsController : ApiController
  {
    public IEnumerable<GroupModel<TeamModel>> Get()
    {
      var service = new JsonGroupsService(@"C:\Users\mattia.richetto\Dropbox\dotNet\prj\FWCB2014\FWCB2014.Application.Web\App_Data\Groups.json", new JsonCountryRepository(@"C:\Users\mattia.richetto\Dropbox\dotNet\prj\FWCB2014\FWCB2014.Application.Web\App_Data\Countries.json"));
      var groups = service.GetAll();
      return groups;
    }
  }
}
