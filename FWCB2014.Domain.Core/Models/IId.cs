namespace FWCB2014.Domain.Core.Models
{
  public interface IId<T>
  {
    T Id { get; set;  }
  }
}