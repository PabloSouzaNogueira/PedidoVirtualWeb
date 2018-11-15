using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PedidoVirtualWeb.Startup))]
namespace PedidoVirtualWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
