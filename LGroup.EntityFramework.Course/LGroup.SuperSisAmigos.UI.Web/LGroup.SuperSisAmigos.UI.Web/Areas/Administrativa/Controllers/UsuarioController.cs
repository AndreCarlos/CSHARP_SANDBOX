using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LGroup.SuperSisAmigos.Model;

namespace LGroup.SuperSisAmigos.UI.Web.Areas.Administrativa.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginMOD dadosTela)
        {
            //Para saber se o usuário deabilitou o JS, utilizamos a propriedade ISVALID
            //Se por acaso, 1 ou 2 campos chegarem em branco, significa que o usuário desabilitou o JS, 
            //se não desabilitar, o JS vem preenchido
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("Listar", "Amigo", new { Area = "" });
            
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
	}
}