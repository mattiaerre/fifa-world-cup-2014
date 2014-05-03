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
            var container = new WindsorContainer(new XmlInterpreter());

            container.Register(Component.For<IImport>().ImplementedBy<CounterImportService>().LifestyleSingleton());

            container.Register(Component.For<ICountriesImportService>().ImplementedBy<CountriesImportService>().DependsOn(new
            {
                source = new Dictionary<string, string> { { "+39", "italy" } },
                basePath = string.Format(@"{0}\Data", Settings.Default.SyndicationRoot),
                baseRestUri = Settings.Default.RestCountriesUri,
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