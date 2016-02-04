using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_ticaret.Startup))]
namespace E_ticaret
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
