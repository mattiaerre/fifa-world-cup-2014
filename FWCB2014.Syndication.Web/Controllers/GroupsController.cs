using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Syndication.Core.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace FWCB2014.Syndication.Web.Controllers
{
    public class GroupsController : ApiController
    {
      private readonly IGroupRepository _repository;

      public GroupsController(IGroupRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<GroupModel> Get()
        {
            return _repository.Find(e => true);
        }

        public IEnumerable<GroupModel> Get(string id)
        {
            return _repository.Find(e => e.GroupId == id);
        }
    }
}
