namespace FWCB2014.Domain.Core.Models
{
  public class TeamModel : ICode
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Alpha2Code { get; set; }
    public string Alpha3Code { get; set; }
  }
}