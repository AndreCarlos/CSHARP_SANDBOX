using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//RESGISTROS
using LGroup.SuperSisAmigos.Model;

//MANIPULAÇÃO DE REGISTROS *CRUD*
using LGroup.SuperSisAmigos.Repository;

namespace LGroup.SuperSisAmigos.UI.Web.Controllers
{
    //Habilitamos um filtro de erro, pra redirecionar o usuário pra página ERROR.CSHTML
    [HandleError]
    public class AmigoController : Controller
    {
        //Criamos um comando que vai ser acessado a partir do JS com AJAX
        public Boolean ValidarEmail(string email)
        {
            //Verificamos se o e-mail digitado já EXISTE
            var repositorio = new AmigoREP();
            var amigos = repositorio.Listar();

            //O comando ANY retorna um TRUE ou FALSE. Se uma determinada informação existir na Tabela, ele retorna TRUE
            //Se não, ele retorna FALSE
            //SINGLE -> TRAZ o registro
            //ANY -> só verifica SE EXISTE
            return (amigos.Any(onde => onde.Email == email));
            
        }
        public ActionResult Listar()
        {
            //throw new Exception("deu erro");
            //Criamos uam variável apontando pro repositório de AMIGOS
            var respositorio = new AmigoREP();
            var amigos = respositorio.Listar();

            //MODEL BINDER
            return View(amigos);
        }

        public ActionResult Cadastrar()
        {
            CarregarSexos();

            return View();
        }

        private void CarregarSexos()
        {
            var repositorioSexo = new SexoREP();
            var sexos = repositorioSexo.Listar();

            ViewBag.Sexos = new SelectList(sexos, "Codigo", "Descricao");
        }


        //Sincronizamos a classe de modelos que foi colocada via MODEL BINDER lá na TELA (VIEW)
        [HttpPost]
        public ActionResult Cadastrar(AmigoMOD dadosTela)
        {
            try
            {
                var repositorio = new AmigoREP(dadosTela);
                repositorio.Cadastrar();

                TempData.Add("SUCESSO", "Amigo cadastrado com Sucesso!");
                return RedirectToAction("Listar");
            }
            catch (Exception erro)
            {
                TempData.Add("ERRO", erro.Message);
                CarregarSexos();

                return View();
            }
        }

        public ActionResult Excluir(Int32 codigo)
        {
            try
            {
                var repositorio = new AmigoREP();
                repositorio.Remover(codigo);

            TempData.Add("SUCESSO", "Amigo excluído com Sucesso!!");
            
            }
            catch (Exception erro)
            {
                TempData.Add("ERRO", erro.Message);
            }
            return RedirectToAction("Listar");
        }

        public ActionResult Editar(Int32 id, Int32 idSexo)
        {
            var repositorio = new AmigoREP();
            var amigo = repositorio.Listar().Single(x => x.Codigo == id);
            
            CarregarSexos();

            return View(amigo);
        }

        [HttpPost]
        public ActionResult Editar(AmigoMOD dadosTela)
        {
            try
            {
                var repositorio = new AmigoREP(dadosTela);
                repositorio.Atualizar();

                TempData.Add("SUCESSO", "Amigo atualizado com Sucesso!");

                return RedirectToAction("Listar");
            }
            catch (Exception erro)
            {
                TempData.Add("ERRO", erro.Message);
                CarregarSexos();

                return View();
            }
        }
	}
}