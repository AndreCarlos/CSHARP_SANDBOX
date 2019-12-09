using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LGroup.SuperSisAmigos.UI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //As rotas saõ formas de navegar no projeto
            //SEM mandar QUERYSTRING ?codigo=1&sexo=1
            //São formas mais elegantes de navegação, podemos criar quantas rotas forem necessárias, nunca criar rotas com a 
            //mesma quantidade de parâmetros
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{codigo}",
                defaults: new { controller = "Home", action = "Index", codigo = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AmigoSexo",
                url: "{controller}/{action}/{id}/{idSexo}"
            );
        }
    }
}
