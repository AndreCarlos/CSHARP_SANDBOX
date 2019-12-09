using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LGroup.SisClientes.UI.Web.ViewModels;
using LGroup.SisClientes.DataTransferObject;
using LGroup.SisClientes.DataAccessObject.Contracts;
using LGroup.SisClientes.DataAccessObject.Implementation;


///Subimos para memoria a ferramenta (framework) de mapeamento de classes
using FastMapper;

namespace LGroup.SisClientes.UI.Web.Controllers
{
    /// <summary>
    /// Quando aprendemos a programar, aprendemos que temos que dar NEW nas classes
    /// uma classe dá new em outra classe
    /// Até porque se não der NEW não funciona(NULL REFERENCE)
    /// Isso sob a perspectiva de arquitetura e boas práticas é errado
    /// Isso é o que chamamos de forte dependência, forte acoplamento
    /// Uma classe fica responsável por inicializar outra classe
    /// Sabendo que é má prática dar NEW, temos que criar um módulo injetor
    /// um módulo de inicialização de classes para que uma classe não fique 
    /// refém (dependente) de outra classe.
    /// 
    /// Dar new em model, viewmodel, dto é de boas (armazenamento de dados)
    /// O que não pode é dar new em classes de NEGÓCIO, ACESSO A DADOS, UTILITÁRIAS
    /// O padrão IOC(Inversão de Controle) nos auxilia a programar de forma que as 
    /// classes nao fiquem reféns umas das outras, basicamente nos auxilia a tirar os NEWS
    /// Para implementar o padrão IOC vamos utilizar a técnica de DI (Injeção de Dependência)
    /// Dá para aplicar o padrão IOC de duas formas: ou com DI ou com SERVICE LOCATOR
    /// </summary>
    public sealed class ClienteController : Controller
    {
        private readonly ClienteDAO _dadosCliente;

        /// <summary>
        /// Aplicamos o padrão IOC 
        /// A classe vem de fora via CONSTRUTOR 
        /// </summary>
        /// <param name="dadosCliente_"></param>
        public ClienteController(ClienteDAO dadosCliente_)
        {
            _dadosCliente = dadosCliente_;
        }

        /// <summary>
        /// O Controller aciona a camada de dados (DAO)
        /// </summary>
        /// <returns></returns>
        public ActionResult Listar()
        {
            ///Trouxemos a lista de clientes do banco de dados
            var clientesTabela = _dadosCliente.Listar();

            ///Transferimosos dados do DTO para o ViewModel
            var clientesTela = TypeAdapter.Adapt<IEnumerable<ClienteDTO>, IEnumerable<ClienteViewModel>>(clientesTabela);

            ///Quando os dados VEM da TABELA, vem dentro de 1 DTO
            ///Para levar esses dados la para tela, temos que jogar dentro de 1 VIEWMODEL
            ///VIEWMODEL é para TELA
            ///DTO é para as TABELAS
            ///Temos que fazer um de para para pegar os dados do DTO e levar pro VIEWMODEL
            ///Pegar de 1 classe e levar para outra 
            ///Para fazer essa transferencia de dados entre as classes vamos usar um Framework
            ///de mercado chamado FASTMAPPER
            ///Ele é de 4 a 12 vezes mais rápido que o AUTOMAPPER

            return View(clientesTela);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ClienteViewModel novoCliente_)
        {
            ///Os dados vem da tela dentro de 1 viewmodel
            ///para levar esses dados para tabela, temos que jogar dentro
            ///de um DTO (de para) entre as classes
            ///FASTMAPPER 
            var clienteTabela = TypeAdapter.Adapt<ClienteViewModel, ClienteDTO>(novoCliente_);

            _dadosCliente.Cadastrar(clienteTabela);

            return RedirectToAction("Listar");
        }
    }
}