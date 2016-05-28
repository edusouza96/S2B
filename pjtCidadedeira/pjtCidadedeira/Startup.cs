using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pjtCidadedeira.Startup))]
namespace pjtCidadedeira
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
