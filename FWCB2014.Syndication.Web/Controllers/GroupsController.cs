using FWCB2014.Domain.Core.Models.Query.Groups;
using System.Collections.Generic;
using System.Web.Http;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Syndication.Web.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly IRepository<GroupModel> _repository;

        public GroupsController(IRepository<GroupModel> repository)
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
