using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Services
{
    [Obsolete("try to use a repository instead", true)]
    public interface IGroupsService<out T>
    {
        IEnumerable<T> GetAll(); // todo: use FindAll() instead
        IEnumerable<T> Find(Func<T, bool> predicate); // todo: move into another interface
    }
}