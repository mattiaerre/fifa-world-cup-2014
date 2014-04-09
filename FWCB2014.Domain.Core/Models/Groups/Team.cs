namespace FWCB2014.Domain.Core.Models.Groups
{
  public class Team : IName, ICode
  {
    public string Name { get; set; }
    public string Code { get; set; }
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