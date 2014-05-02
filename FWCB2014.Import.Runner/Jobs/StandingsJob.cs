using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Runner.Jobs
{
    public class StandingsJob : JobBase
    {
        public StandingsJob(IImport import)
            : base(import, "standings")
        {
        }
    }
}