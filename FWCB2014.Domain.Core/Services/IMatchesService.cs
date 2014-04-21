using System.Collections.Generic;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Command.Matches;

namespace FWCB2014.Domain.Core.Services
{
  public interface IMatchesService<out T> where T : MatchModelBase
  {
    IEnumerable<T> GetAll();
  }
}