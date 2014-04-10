using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Models
{
  public class GroupModel<T> : IName where T : TeamModelBase
  {
    public string Name { get; set; }
    public IEnumerable<T> Teams { get; set; }
  }
}