using System;
using System.Collections.Generic;
using System.Linq;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  [Obsolete("every context will use its own implementation of the repositories", true)]
  public abstract class CountryRepositoryBase : IFind<CountryModel>, IAdd<string>, IDelete
  {
    protected abstract IEnumerable<CountryModel> Countries { get; }

    public abstract void Add(string model);

    public IEnumerable<CountryModel> Find(Func<CountryModel, bool> predicate)
    {
      var countries = Countries.Where(predicate);
      return countries;
    }

    public abstract void Delete();
  }
}