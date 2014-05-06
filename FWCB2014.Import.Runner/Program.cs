using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Infrastructure.Services;
using FWCB2014.Import.Runner.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;
using FWCB2014.Import.Runner.Services;

namespace FWCB2014.Import.Runner
{
  public class Program
  {
    private static IWindsorContainer BootstrapContainer()
    {
      var standingsFeedUri = string.Format("http://api.xmlscores.com/standings/?f=xml&c=wc_2014&open={0}", Settings.Default.ApiKey);
      var syndicationAppDataPath = string.Format(@"{0}\{1}", Settings.Default.SyndicationRoot, "App_Data");

      var container = new WindsorContainer(new XmlInterpreter());

      container.Register(Component.For<IImport>().ImplementedBy<CounterImportService>().LifestyleSingleton());

      container.Register(Component.For<ICountriesImportService>().ImplementedBy<CountriesImportService>().DependsOn(new
      {
        source = new Dictionary<string, string> { { "+39", "italy" } },
        basePath = syndicationAppDataPath,
        baseRestUri = Settings.Default.RestCountriesUri,
      }));

      container.Register(Component.For<IStandingsImportService>().ImplementedBy<StandingsImportService>().DependsOn(new
      {
        feedUri = standingsFeedUri,
        basePath = syndicationAppDataPath,
      }));

      return container;
    }

    public static void Main(string[] args)
    {
      Console.WriteLine("{0} started at {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now.ToString(Settings.Default.DateTimeFormat));
      Console.WriteLine("press any key if you want to close the program");

      var container = BootstrapContainer();

      Console.ReadLine();

      container.Dispose();
    }
  }
}