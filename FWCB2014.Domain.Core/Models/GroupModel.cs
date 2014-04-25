using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Models
{
  public class GroupModel<T> : ICompetitionCode, ISeasonCode, IName where T : StandingModelBase
  {
    public string CompetitionCode { get; set; }
    public string SeasonCode { get; set; }
    public string Name { get; set; }
    public IEnumerable<T> Teams { get; set; } // todo: this should be more generic (as a name) for example Items ???
  }
}