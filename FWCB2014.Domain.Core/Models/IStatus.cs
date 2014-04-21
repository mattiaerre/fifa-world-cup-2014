namespace FWCB2014.Domain.Core.Models
{
  public interface IStatus<T>
  {
    T Status { get; set; }
  }
}