using System.Collections.Generic;
using System.Web.Http;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Syndication.Web.Controllers
{
    public class CountriesController : ApiController
    {
      private readonly IRepository<CountryModel> _repository;

      public CountriesController(IRepository<CountryModel> repository)
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
