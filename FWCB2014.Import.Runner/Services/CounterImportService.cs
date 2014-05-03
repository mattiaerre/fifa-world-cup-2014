using System;
using FWCB2014.Import.Core.Services;

namespace FWCB2014.Import.Runner.Services
{
  public class CounterImportService : IImport
  {
    private int _counter;
    public void Import()
    {
      _counter += 1;
      Console.WriteLine("counter: {0}", _counter);
    }
  }
}