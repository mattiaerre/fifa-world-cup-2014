using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Models.Query.Groups
{
    public class GroupModel : IGroupId<string>
    {
        public string GroupId { get; set; }
        public IEnumerable<StandingModel> Standings { get; set; }
    }
}
