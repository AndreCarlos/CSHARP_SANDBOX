using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LGroup.SuperSisAmigos.UI.Web.Controllers
{
    /// <summary>
    /// Desde o asp 3.0 conseguimos exibir telas mais customizadas de erro. O problema das telas de erro do asp.net e que
    /// sao feias e técnicas, tem informações que somente um profissional de TI entenderia
    /// Sempre que o usuário tomar algum erro na aplicação, vai exibir uma tela mais amigável de erro.
    /// </summary>
    public class ErroController : Controller
    {
        public ActionResult Erro404()
        {
            return View();
        }
    }
}