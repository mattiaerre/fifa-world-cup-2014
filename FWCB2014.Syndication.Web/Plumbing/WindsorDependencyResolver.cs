using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace FWCB2014.Syndication.Web.Plumbing
{
  internal sealed class WindsorDependencyResolver : IDependencyResolver
  {
    private readonly IWindsorContainer _container;

    public WindsorDependencyResolver(IWindsorContainer container)
    {
      if (container == null)
      {
        throw new ArgumentNullException("container");
      }

      _container = container;
    }
    public object GetService(Type t)
    {
      return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
    }

    public IEnumerable<object> GetServices(Type t)
    {
      return _container.ResolveAll(t).Cast<object>().ToArray();
    }

    public IDependencyScope BeginScope()
    {
      return new WindsorDependencyScope(_container);
    }

    public void Dispose()
    {
      //_container.Dispose();
    }
  }
}