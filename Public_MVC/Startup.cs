using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Public_MVC.Startup))]
namespace Public_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
