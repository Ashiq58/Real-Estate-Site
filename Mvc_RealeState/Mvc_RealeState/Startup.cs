using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc_RealeState.Startup))]
namespace Mvc_RealeState
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
