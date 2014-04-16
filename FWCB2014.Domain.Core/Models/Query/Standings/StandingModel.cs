namespace FWCB2014.Domain.Core.Models.Query.Standings
{
  public class StandingModel : StandingModelBase, ITeamModel
  {
    public TeamModel Team { get; set; }
  }
}
