namespace FWCB2014.Domain.Core.Models.Command.Matches
{
  public class TeamModel: IId<string>
  {
    public string Id { get; set; }
    public int Score { get; set; }
  }
}