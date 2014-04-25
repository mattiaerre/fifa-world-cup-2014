using System;

namespace FWCB2014.Domain.Core.Repositories
{
  public interface IRepository<out T>
  {
    T Find(Func<T, bool> predicate);
  }
}