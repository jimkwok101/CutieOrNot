using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CutieOrNot.Startup))]
namespace CutieOrNot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
