using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UWPBackend.Startup))]

namespace UWPBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureMobileApp(app);
        }
    }
}