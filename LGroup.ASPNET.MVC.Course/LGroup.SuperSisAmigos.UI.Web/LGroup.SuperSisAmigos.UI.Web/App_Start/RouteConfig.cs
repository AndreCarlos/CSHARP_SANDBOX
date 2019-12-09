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

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //O asp.net MVC possue 2 view engines (modelos de visualização) 
            //modelo antigo (legado) == ASPX
            //modelo moderninho (novo) == Razor
            //Sempre que pedimos para navegar em uma página mesmo ela sendo feito no novo modelo (razor) ele sempre olha
            //primeiro no modelo antigo (aspx) o que gera um custo (IO)
            //Ja que sabemos que sempre vai ser razor, pensando em performance podemos mandar ignorar o modelo de visualização
            //legado (ASPX)
            //Por padrão vem as duas formas de visualização. Limpamos as duas
            ViewEngines.Engines.Clear();

            //Mandamos adicionar somente a visualização em RAZOR
            //Fica mais rápido porque ele deixa de olhar 4 locais diferentes
            //ASPX, ASCX (pasta do controller, pasta shared)
            ViewEngines.Engines.Add( new RazorViewEngine());
        }
    }
}
