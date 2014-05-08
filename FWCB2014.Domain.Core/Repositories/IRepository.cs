using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Repositories
{
  public interface IRepository<T>
  {
    // C/U
    void Add(IEnumerable<T> models);
    // R
    IEnumerable<T> Find(Func<T, bool> predicate);
    // D
    void Delete();
  }
}