using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Repositories
{
  public interface IRepository<out T>
  {
    IEnumerable<T> Find(Func<T, bool> predicate);
  }
}