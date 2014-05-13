namespace FWCB2014.Import.Core.Models.Matches
{
	public interface IStatus<out T>
	{
		T Status { get; }
	}
}