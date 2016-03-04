using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apply.Startup))]
namespace Apply
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
