using System.Web.Mvc;

namespace LGroup.SuperSisAmigos.UI.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        //Desde o MVC 2.0 temos um recurso de criação de AREAS
        //Areas == Módulos, Seccções do nosso site
        //Blog, Fórum, Departamento, RH, Vendas, TI. A Àrea é uma forma organizada de agrupar os arquivos
        //Tudo que é referente a area admin fica na area de admin do site
        //Quando executarmos o projeto principal ele joga as áreas dentro dele, ele registra as areas dentro
        //do projeto principal
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //Sempre que você quisaer navegar em algum controlle que está dentro da AREA tem que colocar o nome da área
            //na frente
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}