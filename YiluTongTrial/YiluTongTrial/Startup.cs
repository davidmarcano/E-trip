using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YiluTongTrial.Startup))]
namespace YiluTongTrial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
