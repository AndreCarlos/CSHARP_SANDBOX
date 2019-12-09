using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace LGroup.SuperSisAmigos.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //No arquivo Global.Asax temos eventos em nivel de aplicaçao. Assim que a aplicaçao for iniciada, subir para
        //memória ele registra as areas dentro do projeto principal e de quebra registra as rotas (URLs amigáveis)
        //MVC >> APPLICATION >> INFRAESTRUCTURE >> DOMAIN
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ///Assim que a aplicação subiu para memória, mandamos configurar as áreas, as rotas e os 
            ///agrupamentos (bundles)
            LGroup.SuperSisAmigos.UI.Web.App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
