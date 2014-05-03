using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Runner.Jobs
{
  public class MatchesJob : JobBase
  {
    public MatchesJob(IImport import)
      : base(import, "matches")
    {
    }
  }
}
