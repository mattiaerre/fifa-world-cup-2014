using System;
using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Runner.Jobs
{
    public class FakeImportService : IImport
    {
        public void Import()
        {
            Console.WriteLine("I am doing nothing!");
        }
    }
}