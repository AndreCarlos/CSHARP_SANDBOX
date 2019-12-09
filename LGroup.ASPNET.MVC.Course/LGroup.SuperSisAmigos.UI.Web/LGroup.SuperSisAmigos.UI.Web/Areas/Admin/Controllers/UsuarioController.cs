using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//para poder visualizar as classes de viewmodel (armazenamento de dados)
//subimos para memoria a pasta de viewmodel
using LGroup.SuperSisAmigos.UI.Web.Areas.Admin.ViewModels;

namespace LGroup.SuperSisAmigos.UI.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// O atributo HandleError fica monitorando o controller sempre que der qualquer erro, ele automaticamente
    /// vai redirecionar para a página Error.cshtml
    /// HandleError e Error.cshtml trabalham juntos
    /// </summary>
    [HandleError]
    public class UsuarioController : Controller
    {
        #region ACTIONS (GET)
        [OutputCache (Duration = 180)]
        public ActionResult Login()
        {
            //throw new Exception("Erro no banco");
            return View();
        }

        #endregion

        #region ACTIONS (POST)

        //criamso uma action para pegar os dados da tela
        //GET >> DESCE
        //POOST >> VOLTA
        [HttpPost]
        public ActionResult Login(UsuarioViewModel dadosTela)
        {
            /// Tomar cuidado com os Hackers, caso você utilize javascript para validar algum campo
            /// e o hacker desabilite o javascript ele vai conseguir entrar no C#, vai dar um POST e vai burlar a página
            /// (regras, consistencias)
            /// A única forma dos campos de login e senha ficarem em branco aqui  no POST eh o usuário desabilitando
            /// o JS

            ///O ModelState é onde ficam as mensagens de validação 
            ///Required no ErrorMessage ele guarda no modelState
            ///Se todos os campos foram devidamente preenchidos 
            ///O Isvalid == true
            ///Caso algum campo tenha ficado em branco
            ///IsValid == false e mandamos abrir novamente a tela de login, bloqueamos ele na tela de login
            if (!ModelState.IsValid)
            {
                ///Armazenamos uma mensagem de erro para assustar
                ModelState.AddModelError("HACKER", "Login e Senha Requeridos!!");
                return View();
            }

            //Apos loggar no sistema redirecionamos os usuarios para página de listar de amigos
            //Se você for chamar um controller que está fora da area ou sem outra area tem que passar o nome
            //da AREA
            return RedirectToAction("Listar", "Amigo", new { Area = "" });
        }

        #endregion
    }
}