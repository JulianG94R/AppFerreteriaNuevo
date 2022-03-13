using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppFerreteria.Startup))]
namespace AppFerreteria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
