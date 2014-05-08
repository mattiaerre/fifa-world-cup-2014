using System;
using System.Collections.Generic;
using System.Linq;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public abstract class CountryRepositoryBase : IRepository<CountryModel>
  {
    protected abstract IEnumerable<CountryModel> Countries { get; }

    public abstract void Add(IEnumerable<CountryModel> models);

    public IEnumerable<CountryModel> Find(Func<CountryModel, bool> predicate)
    {
      var countries = Countries.Where(predicate);
      return countries;
    }

    public abstract void Delete();
  }
}