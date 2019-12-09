using System.Web.Mvc;

namespace LGroup.SuperSisAmigos.UI.Web.Areas.Administrativa
{
    public class AdministrativaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrativa";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //A Area depende do projeto PRINCIPAL
            //Ela é um módulo que roda acoplado ao PROJETAO
            //Ela também possui uma rota de navegação
            context.MapRoute(
                "Administrativa_default",
                "Administrativa/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}