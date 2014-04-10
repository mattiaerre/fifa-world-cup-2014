using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FWCB2014.Application.Web.Plumbing;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Syndication.Infrastructure.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FWCB2014.Application.Web
{
  public class MvcApplication : System.Web.HttpApplication
  {
    private static IWindsorContainer _container;

    private void BootstrapContainer()
    {
      _container = new WindsorContainer().Install(FromAssembly.This());

      var controllerFactory = new WindsorControllerFactory(_container.Kernel);
      ControllerBuilder.Current.SetControllerFactory(controllerFactory);

      _container.Register(
        Component.For<IRepository<CountryModel>>().ImplementedBy<JsonCountryRepository>().DependsOn(new { jsonPath = Server.MapPath(@"~/App_Data/Countries.json") }));

      _container.Register(
        Component.For<IGroupsService<GroupModel<TeamModel>>>().ImplementedBy<JsonGroupsService>().DependsOn(new { jsonPath = Server.MapPath(@"~/App_Data/Groups.json"), }));
    }

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      BootstrapContainer();
    }

    protected void Application_End()
    {
      _container.Dispose();
    }
  }
}
