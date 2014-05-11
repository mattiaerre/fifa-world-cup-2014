using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace FWCB2014.Syndication.Web.Controllers
{
  [Obsolete("no business value", true)]
  public class CountriesController : ApiController
  {
    private readonly IFind<CountryModel> _repository;

    public CountriesController(IFind<CountryModel> repository)
    {
      _repository = repository;
    }

    public IEnumerable<CountryModel> Get()
    {
      return _repository.Find(e => true);
    }

    public IEnumerable<CountryModel> Get(string id)
    {
      return _repository.Find(e => e.Alpha3Code == id.ToUpper());
    }
  }
}
