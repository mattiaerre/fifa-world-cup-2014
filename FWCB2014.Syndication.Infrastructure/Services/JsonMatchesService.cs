using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCB2014.Domain.Core.Models.Query.Matches;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Core.Services;

namespace FWCB2014.Syndication.Infrastructure.Services
{
  public class JsonMatchesService : IMatchesService<MatchModel>
  {
    private readonly string _jsonPath;
    //private readonly IRepository<TeamModel> _repository;

    public JsonMatchesService(string jsonPath) //, IRepository<TeamModel> repository)
    {
      _jsonPath = jsonPath;
      //_repository = repository;
    }

    public IEnumerable<MatchModel> GetAll()
    {
      throw new NotImplementedException();
    }
  }
}
