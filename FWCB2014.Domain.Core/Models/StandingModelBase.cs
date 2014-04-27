namespace FWCB2014.Domain.Core.Models
{
  public abstract class StandingModelBase : ITeamId<string>, IGroupId<string>
  {
    public string TeamId { get; set; }
    public string GroupId { get; set; }
    public int Position { get; set; }
    public int Played { get; set; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int Scored { get; set; }
    public int Conceeded { get; set; }
    public int Points { get; set; }
  }
}