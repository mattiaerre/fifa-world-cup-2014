using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Models.Groups
{
  public class Group : IName
  {
    public string Name { get; set; }
    public IEnumerable<Team> Teams { get; set; }
  }
}