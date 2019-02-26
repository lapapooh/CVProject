using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INTRANET.Startup))]
namespace INTRANET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
