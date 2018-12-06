using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NEMILTEC.MVC.Startup))]
namespace NEMILTEC.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
