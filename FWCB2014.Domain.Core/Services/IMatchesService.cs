using System.Collections.Generic;
using FWCB2014.Domain.Core.Models.Command.Matches;

namespace FWCB2014.Domain.Core.Services
{
  public interface IMatchesService
  {
    IEnumerable<MatchModel> GetAll();
  }
}