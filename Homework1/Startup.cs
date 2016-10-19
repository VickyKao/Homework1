using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Homework1.Startup))]
namespace Homework1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
