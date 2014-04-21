using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Services
{
  public interface IGroupsService<out T>
  {
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Func<T, bool> predicate); // todo: move into another interface
  }
}