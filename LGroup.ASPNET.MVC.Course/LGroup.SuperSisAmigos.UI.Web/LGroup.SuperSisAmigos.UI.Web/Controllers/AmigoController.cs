using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//subimos para memória a pasta de viewmodels, 
//as classes que aramazenam os dados das View estã lá dentro
using LGroup.SuperSisAmigos.UI.Web.ViewModels;

//subimos as classes de armazenamento de dados (ENTIDADES)
using LGroup.SuperSisAmigos.Entities;

//subimnos as classes de serviço (APPLICATION). São essas classes que acionam a infra e o domain
using LGroup.SuperSisAmigos.Service;

namespace LGroup.SuperSisAmigos.UI.Web.Controllers
{
    [HandleError]
    public sealed class AmigoController : Controller
    {
        /// <summary>
        /// criamos uma variavel para servir os dados de amigo, de sexo e de 
        /// Estado civil
        /// 
        /// AmigoController >> AmigoService >> AmigoRepository >> Conexão >> EF >> Banco
        /// </summary>

        private AmigoService _serviceAmigo = new AmigoService();


        #region ACTIONS (GET)
                public ActionResult Listar()
                {
                    ///Trouxemos todos os amigos lá do banco de dados
                    var amigos = _serviceAmigo.ListarAmigos();

                    ///Quando trouxemos os dados da tabela, eles vêem dentro da classe amigo (DOMAIN MODEL)
                    ///Quando levamos os dados para tela temos uma classe que armazena dados
                    ///de tela (VIEW MODEL)
                    ///Temos que pegar os dados que estão dentro da CLASSE AMIGO e jogar para clsse 
                    ///AMIGOVIEWMODEL (DE-PARA)

                    var listaAmigos = new List<AmigoViewModel>();
                    foreach (var amigo in amigos)
                    {
                        ///Para cada amigo que encontramos, transformamos de AMIGO para AMIGOVIEWMODEL
                        var vmAmigo = new AmigoViewModel();

                        vmAmigo.CodigoAmigo = amigo.Codigo;
                        vmAmigo.Nome = amigo.Nome;
                        vmAmigo.Email = amigo.Email;
                        vmAmigo.DataNascimento = amigo.DataNascimento;
                        vmAmigo.Telefone = amigo.Telefone;

                        listaAmigos.Add(vmAmigo);
                    }
                    //Fizemos o Model Binding e levamos para tela
                    return View(listaAmigos);
                }

                /// <summary>
                /// Quando uma tela eh enviada para o navegador, internamente tem todo um pipeline (fluxo de execução) e ele abre o 
                /// arquivo cadastrar.cshtml 
                /// Para ganharmos performance na abertura de paginas, podemos deixar as páginas no cache (memória RAM do servidor). As páginas
                /// ficam pré-carregadas na memória, assim as próximas execuções as próximas navegadas nessa páginas serão executadas
                /// mais rápidas
                /// 
                /// Utilizar esta técnica em páginas que nao mudam com frequencia: Login, Home, Fale conosco, Endereço da empresa, Cadastrar
                /// É uma técnica de performance do ASP.NET MVC
                /// </summary>
                /// 
                ///conseguimos controlar o tempo de cache da página. O tempo é baseadop em segundo = 1 minuto = 60 segundos
                ///Acabou o tempo ele se renova, colocamos por 3 minutos        
                [OutputCache(Duration = 180)]
                public ActionResult Cadastrar()
                {
                    return View();
                }

                public ActionResult Editar()
        {
            return View();
        }

        #endregion


        #region ACTIONS (POST)
        
                //Criamos uma action para resgatar os dados da tela de cadastrar, quando dar o submit
                //vai bater aqui dentro:
                //1 action vai (get) e 1 action volta (Post)

                //OBS: Se você criou a tela a partir de uma classe de model, viewmodel
                //pra resgatar os dados da tela temos que pegar aquela mesma classe nade de FORMCOLLECTION MUITO MENOS NAME
                [HttpPost]
                public ActionResult Cadastrar(AmigoViewModel dadosTela)
                {
                    //Antes de redirecionar o usuario para tela de listar criamos uma mensagem de sucesso
                    //no TEMPDATA. 
                    //VIEWBAG -- Pra levar dados pra propria pagina aberta
                    //TEMPDATA -- Pra levar dados pra outra página, ação 
                    TempData.Add("Sucesso", "Amigo Cadastrado com Sucesso!");

                    //Assim que terminou de cadastrar (insert) levamos o usuario para tela de listar
                    return RedirectToAction("Listar");
                }

                [HttpPost]
                public ActionResult Editar(AmigoViewModel dadosTela)
        {
            TempData.Add("Sucesso", "Amigo Atualizado com Sucesso!");

            return RedirectToAction("Listar");
        }

        #endregion
    }
}