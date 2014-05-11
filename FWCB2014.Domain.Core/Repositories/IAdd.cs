namespace FWCB2014.Domain.Core.Repositories
{
  public interface IAdd<in T>
  {
    void Add(T model);
  }
}