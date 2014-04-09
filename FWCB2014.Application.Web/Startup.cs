using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FWCB2014.Application.Web.Startup))]
namespace FWCB2014.Application.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
