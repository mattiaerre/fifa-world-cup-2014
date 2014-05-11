using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FWCB2014.Syndication.Core.Repositories;
using FWCB2014.Syndication.Infrastructure.Repositories;
using FWCB2014.Syndication.Web.Plumbing;
using FWCB2014.Syndication.Web.Properties;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiContrib.IoC.CastleWindsor;

namespace FWCB2014.Syndication.Web
{
  public class WebApiApplication : HttpApplication
  {
    private static IWindsorContainer _container;

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      BootstrapContainer(GlobalConfiguration.Configuration);
    }

    private void BootstrapContainer(HttpConfiguration configuration)
    {
      _container = new WindsorContainer().Install(FromAssembly.This());

      var controllerFactory = new WindsorControllerFactory(_container.Kernel);
      ControllerBuilder.Current.SetControllerFactory(controllerFactory);

      _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));

      // info: for ApiController
      var dependencyResolver = new WindsorResolver(_container);
      configuration.DependencyResolver = dependencyResolver;
      _container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient());
      // /info: for ApiController

      _container.Register(
        Component.For<ICountryRepository>()
        .ImplementedBy<CountryRepository>()
        .DependsOn(new
        {
          connectionString = Settings.Default.StorageConnectionString,
          tableName = "countries",
          partitionKey = "wc",
          rowKey = "2014",
        })
        .LifestyleSingleton());

      _container.Register(
        Component.For<IGroupRepository>()
        .ImplementedBy<GroupRepository>()
        .DependsOn(new
        {
          teamCountryMappingJsonPath = Server.MapPath(@"~/App_Data/Team_Country_Mapping.json"),
          standingsJsonPath = Server.MapPath(@"~/App_Data/Standings.json"),
        })
        .LifestyleSingleton());
    }

    protected void Application_End()
    {
      _container.Dispose();
    }
  }
}
