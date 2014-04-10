using FWCB2014.Domain.Core.Models;

namespace FWCB2014.Domain.Core.Repositories
{
  public interface IRepository<out T> where T : ICode
  {
    T Find(string code);
  }
}