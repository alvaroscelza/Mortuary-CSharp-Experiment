using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(mortuary.Startup))]
namespace mortuary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
