using System;
using System.Collections.Generic;
using FWCB2014.Domain.Core.Models;

namespace FWCB2014.Domain.Core.Services
{
  [Obsolete("try to use a repository instead", false)]
  public interface IMatchesService<out T> where T : MatchModelBase
  {
    IEnumerable<T> GetAll();
  }
}