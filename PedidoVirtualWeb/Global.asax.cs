using PedidoVirtualWeb.App_Start;
using PedidoVirtualWeb.Classes;
using PedidoVirtualWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PedidoVirtualWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.PedidoVirtualContext, Migrations.Configuration>());

            CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //PedidoBase.Items = new List<int>();
            //PedidoBase.Pedido = 0;
            PedidoBase.Carrinho = new List<PedidoItem>();
        }

        private void CheckRolesAndSuperUser()
        {
            UserHelper.CheckRole("Admin");
            UserHelper.CheckRole("User");
            UserHelper.CheckSuperUser();

        }
    }
}
