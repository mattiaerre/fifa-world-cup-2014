namespace FWCB2014.Import.Core.Models.Matches
{
	public interface IId<out T>
	{
		T Id { get; }
	}
}