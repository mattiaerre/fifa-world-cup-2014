using System;

namespace FWCB2014.Domain.Core.Models.Command.Matches
{
  [Obsolete("do not use this", true)]
  public class TeamModel: IId<string>
  {
    public string Id { get; set; }
    public int Score { get; set; }
  }
}