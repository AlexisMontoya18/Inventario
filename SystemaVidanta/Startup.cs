using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SystemaVidanta.Startup))]
namespace SystemaVidanta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
