using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Models
{
    [Obsolete("use FWCB2014.Domain.Core.Models.Query.Groups.GroupModel instead", true)]
    public class GroupModel<T> : ICompetitionCode, ISeasonCode, IName where T : StandingModelBase // info: why this ?!?
    {
        public string CompetitionCode { get; set; }
        public string SeasonCode { get; set; }
        public string Name { get; set; }
        public IEnumerable<T> Teams { get; set; } // todo: this should be more generic (as a name) for example Items ???
    }
}