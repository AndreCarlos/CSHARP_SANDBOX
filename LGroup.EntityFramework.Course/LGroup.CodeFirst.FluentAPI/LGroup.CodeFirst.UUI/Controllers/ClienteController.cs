using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//pra poder recuperar os dados da tela importamos a namespace
//de modelos (armazenamento de dados)
using LGroup.CodeFirst.Model;

//subimos para memoria a dll de manipulação das tabelas
//onde fica a abertura de conexao eo CRUD
using LGroup.CodeFirst.Repository;
using LGroup.CodeFirst.Repository.Contracts;

//subimos para memoria a DLL que agrupa os repositorios (organizacao)
//fluxo ==> CONTROLER >> "BUSINNESS ACCESS" >> UOW >> REPOSITORIES >> DATAACCESS
using LGroup.CodeFirst.UnitOfWork;

//new somente em viewmodel ou classe de models

namespace LGroup.CodeFirst.UUI.Controllers
{
    //toda inteligencia de carregamento e abertura de telas fica
    //dentro de controller (classe controladora), lembra o aspx.cs 
    //(code behind) lá do web forms
    public class ClienteController : Controller
    {
        //criamos uma variavel apontando (referenciando) o repositorio de clientes - crud == tabelas
        //private ClienteRepository _dadosCliente = new ClienteRepository();
     
        //quando fazerrmos a IoC se a classe possuir um super tipo : Classe ou Interface pai
        //para ficar mais flexivel, mais generico sempre colocar o nome do Super Tipo
        private PedidoUnitOfWork _unidadePedido;

        //existe um padrao de projeto chamado de inversao de controle 
        //Apelido carinhoso IoC
        //Ele fala que uma classe nao pode ser responsavel por inicializar outra classe 
        //uma classe nao pode dar new em outra classe, pois se fizermos isso estamos criando um 
        //forte acoplamento, uma dependencia entre as 2 classes
        //pra utilizarmos essa boa pratica (padrao IoC) temos que utilizar a tecnica de injeçao de 
        //dependcia (DI)
        //prós    : senioridade, manutenção do código, testabilidade
        //contras : fica bem mais lento e dificil de entender (NEW)

        public ClienteController(PedidoUnitOfWork unidadePedido_)
        {
            _unidadePedido = unidadePedido_;
        }

       //pra cada pagina que quisermos abrir, mostrar pro usuario
        //temos que criar um apelido (action == ação)

        public ActionResult Listar()
        {
            //temos que acionar o repositorio e pegar os dados dos clientes e enviar pra tela
            //(SELECT * FROM TB_CLIENTE)    
            var clientes = _unidadePedido.RepositorioCliente.Listar();

            //é nesse momento que repassamos pra tela
            //pra enviar dados pra tela temos 3 formas:
            //VIEW() - MODEL BINDING (pra mandar os principais dados da tela)
            //VIEWBAG() - dados secundarios, informaçoes complementares, titulo, texto de botao, combobox
            //TEMPDATA() - pra mandar dados de 1 tela pra outra
            return View(clientes);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        //normalmente no mvc criamos 2 actons por tela: uma acction para abrir a tela
        //e outra actiuon para pegara os dados da tela
        // o mvc eh magico, ele automaticamente pega os dados que o usuario digitou na tela, salva
        //dentro dos campos da classe clientemodel e traz aqui pra dentro do comando cadastrar (model binding)
        //DDD ==> mvc -> application -> infra -> domain
        //aqui ==> mvc -> repository -> data access -> model
        [HttpPost]
        public ActionResult Cadastrar(ClienteModel dadosTela)
        {
            //pegamos os dados que vieram da tabela e passamos pra dentro
            //do repositorio de clientes (insert into tb_cliente)

            //view -> controller -> repository -> data access -> EF -> banco 
            _unidadePedido.RepositorioCliente.Cadastrar(dadosTela);

            TempData.Add("SUCESSO", "Cliente Cadastrado com Sucesso");

            //apos cadastar o cliente, levamos o usuario prar tela de lista 
            //Response.Redirect();
            return RedirectToAction("Listar");
        }

        //criamos mais duas actions. Eh nelas que vao ficar a intetligencia (codigo) de excluir e editar
        public ActionResult Excluir(Int32 id)
        {
            //acionamos o repositorio passando o codigo do cliente que veio da tela dentro da url pra
            //fazer a exclusao na tabela
            _unidadePedido.RepositorioCliente.Deletar(id);

            TempData.Add("SUCESSO", "Cliente Excluído com Sucesso");

            return RedirectToAction("Listar");
        }

        public ActionResult Editar(Int32 id)
        {
            //pegamos o codigo do cliente que veio da tela de listar e batemos
            //no repositorio e descobrimos o restante dos dados (cliente)

            var cliente = _unidadePedido.RepositorioCliente.Pesquisar(x => x.Codigo == id).Single();

            //descemos o cliente para tela
            return View(cliente);
        }

        //criamos uma action POST pra pegar os dados que o usuário digitou
        //dentros dos campos da tela


        //o MVC trabalha com a técnica de MODEL BINDING - sincronização de telas com classes
        //tudo que fizermos nos campos da tela automaticamente serão salvos nos campos da classe
        [HttpPost]
        public ActionResult Editar(ClienteModel dadosTela)
        {
            // o bom de trabalhar com dlls (camadas) eh que soh programamos
            //uma vez e reutilizamos em todos os outros projetos
            _unidadePedido.RepositorioCliente.Atualizar(dadosTela);

            //criamos uma variavel dentro do tempdata(eh um local de armazenamento de dados temporario e 
            //exclusivo do MVC
            //VIEWBAG  para levar dados da pagina pai ou pra propria pagina
            //TEMPDATA para levar dados da outra action ou outra view

            TempData.Add("SUCESSO", "Cliente Atualizado com Sucesso");

            return RedirectToAction("Listar");
        }
    }
}