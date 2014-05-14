namespace FWCB2014.Import.Core.Models.Matches
{
  public class TeamModel : IId<string>
  {
    public string Id { get; private set; }
    public int Score { get; set; }

    public TeamModel(string id)
    {
      Id = id;
    }
  }
}