using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portfolio.WebUI.Startup))]
namespace Portfolio.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
