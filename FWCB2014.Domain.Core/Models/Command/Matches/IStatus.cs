namespace FWCB2014.Domain.Core.Models.Command.Matches
{
  public interface IStatus<T>
  {
    T Status { get; set; }
  }
}