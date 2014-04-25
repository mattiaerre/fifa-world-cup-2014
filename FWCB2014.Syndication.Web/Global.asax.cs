using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Standings;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Syndication.Infrastructure.Services;
using FWCB2014.Syndication.Web.Plumbing;
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
        Component.For<IRepository<CountryModel>>().ImplementedBy<JsonCountryRepository>().DependsOn(new { jsonPath = Server.MapPath(@"~/App_Data/Countries.json") }));

      _container.Register(
        Component.For<IGroupsService<GroupModel<StandingModel>>>().ImplementedBy<JsonGroupsService>().DependsOn(new { jsonPath = Server.MapPath(@"~/App_Data/Groups.json"), }));
    }

    protected void Application_End()
    {
      _container.Dispose();
    }
  }
}
