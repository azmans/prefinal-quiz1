using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreFinal.Startup))]
namespace PreFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
