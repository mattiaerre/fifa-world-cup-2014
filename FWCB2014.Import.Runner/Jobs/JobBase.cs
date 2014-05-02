using System;
using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Runner.Properties;
using Quartz;

namespace FWCB2014.Import.Runner.Jobs
{
    public abstract class JobBase : IJob
    {
        private readonly IImport _import;
        private readonly string _entitiesToImportName;

        protected JobBase(IImport import, string entitiesToImportName)
        {
            _import = import;
            _entitiesToImportName = entitiesToImportName;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _import.Import();
                Console.WriteLine("{0} {1}.Info message: {2} have been imported", DateTime.Now.ToString(Settings.Default.DateTimeFormat), GetType().Name, _entitiesToImportName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}.Error message: {2}", DateTime.Now.ToString(Settings.Default.DateTimeFormat), GetType().Name, ex.Message);
            }
        }
    }
}