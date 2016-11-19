using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Acctive.Startup))]

namespace Acctive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}