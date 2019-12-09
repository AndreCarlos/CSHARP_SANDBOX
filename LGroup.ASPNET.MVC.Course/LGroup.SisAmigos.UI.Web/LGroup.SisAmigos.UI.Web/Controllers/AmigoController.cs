using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/* * 
    Desde 2008 o que tem de mais TOP para fazer acesso a dados Microsoft é o Entity Framnework (EF)
    no VS2013 temos disponiveis aa versóes 5.0 e 6.x
    ==> É um EDMX (EF) por banco de dados
    Nome mais senior - geramos o EDMX através da estratégia DataBase First (primeiro montamos o banco de dados
    e depois geramos o EF)
    Subimos o diretório para memória para visualizar o Entity Framework
 * */
using LGroup.SisAmigos.UI.Web.DataAccess;

/* *
    Quando instalamos o visual studio 2013, vem como padrao o MVC 5.0
    uma novidade do 2013 agora temos um unico template de criaçao de projetos web >> ASP.NET ONE. 
    Todos os 3 aspnets foram integrados em 1 único projeto
    WEB FORMS (lixo), MVC (novo) e WEB API (serviços)
 * */
namespace LGroup.SisAmigos.UI.Web.Controllers
{
    /* *
        A classe controladora serve para declarar as paginas que queremos abrir,
        navegar, exibir para o usuario. É aqui dentro que criamos os apelidos das páginas
        e programaremos as páginas (acesso a banco, negocios, etc)
        nunca matar a palavra controller. É uma convenção, é um padrao de nomenclatura interno asp.net.
        Sem ele dá erro
     * */
    public class AmigoController : Controller
    {
        /* *
            sempre que quisermos abrir páginas, temos que criar apelidos pra essas páginas
            nome => ACTION OU AÇÃO

            antes de criar as paginas(html, css, js), primeira coisa que temos que fazer são
            as actions, cada  action vai ser responsavel por gerar, abrir uma pagina
         * */
        public ActionResult Listar()
        {
            /* *
                Criamos um objeto (variavel) apontando a classe de conexao (Contexto)
                do EF (mapeamento e representaçao de banco de dados)
                Dica:
                para descobrir o nome da classe de conexao, digitar ENTITIES. Há uma unica palavra NOMEDOBANCOEntities
               Ex.:
                MICROSOFT -> MICROSOFTEntities
                NETCODERS -> NETCODERSEntities
            * */
            var conexao = new AGENDAEntities();

            /* *
                Demos um select * from tb_amigo, pegamos todos os registros e descemos para View
                Eh o comando ToList() que abre a conexao com o BD e levou o comando
                SELECT (TSQL) para o servidor de banco de dados
            * */
            var amigos = conexao.TB_AMIGO.ToList();

            /* *
                podemos navegar no projeto mesmo sem ter paginas. É a técnica do ASPNET MVC chamada
                de routing(rotas), criamos url amigaveis pra abrir as páginas

                a pagina que ele tenta abrir tem o mesmo nome da action(Listar.cshtml).
                quando mandamos o comando return view() eh nesse momento que ele abre a pagina e devolve pro
                navegador

                Uma das formas de descer os dados para view eh através da técnica chamada MODEL BINDING
                passamos os dados dentro do comando view
                Via MODEL BINDING podemos descer dados complexos (lista de registros, new numa classe qualquer) e até 
                mesmo dados simples (primitivos) int, datetime, boolen soh dah pra descer uma unica informação via MODEL BINDING
            * */
            return View(amigos);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        /* *
             o terceiro parametro eh sempre o id, pois foi definido lá no ROUTECONFIG(rota padrão), a única forma 
             de colocar outro nome eh alterado lá no routeconfig de ID para qualquer outro nome
         * */
        public ActionResult Editar(Int32 id)
        {
            //atraves do ID que veio da tela de listar, temos que dar um select na tabela e trazer o restante
            //dos campos (nome, email, telefone e data)
            var conexao = new AGENDAEntities();

            //o comando FIND do EF busca sempre o campo que eh chave primária (PK)
            //SELECT * FROM TB_AMIGO WHERE ID_AMIGO = ID
            var amigo = conexao.TB_AMIGO.Find(id);

            //além de levar para tela os dados do amigo temos que levar também os dados da table de sexo
            var sexos = conexao.TB_SEXO.ToList();

            //como já enviamos uma informacao via MODEL BINDING já queimou o cartucho
            //tudo que for informação secundaria, complementar descemos via VIEWBAG
            //temos 3 formas de descer dados para tela
            //1 >> MODEL BINDING = Pra propria TELA, dados principais da tela (AMIGO)
            //2 >> VIEWBAG = Pra propria TELA, dados secundarios (titulo, txt botao, rodapé)
            //3 >> TEMPDATA = Pra outra TELA

            //o VIEWBAG também eh exclusivo do MVC, também eh temporario (como tempdata)
            //cuidado com o viewbag, ele eh do tipo DYNAMIC (nao tem intellisense) eh na raça
            //ele soh verifica se o que esta lá dentro eh certo ou errado quando for compilar

            //para carregarmos um combobox (dropdownlist) a partir do controller temos que usar
            //a classe selectlist (exclusiva para carregamento do combo)
            //1 >> Dados que vao dentro do combo
            //2 >> campo que eh chave primaria (value) (numero oculto por tras do campo)
            //3 >> campo que eh descricao, o texto que vai aparecer para o usuario
            //4 >> campo onde falamos o sexo que vai selecionado, se 1-fem se 2-masc
            ViewBag.ListaSexos = new SelectList(sexos, "ID_SEXO", "DS_SEXO",amigo.ID_SEXO);

            //Após trazer o registro da table, levamos ele via MODEL BINDING la na tela de EDITAR
            return View(amigo);
        }

        /* *
            Normalmente criamdos duas actions por página (Cadastrar e Editar)
            >> 1 action para abrir a tela e levar dados para ela
            >> 1 action para resgatar os dados da tela (dos campos de preenchimento)
            GET => LEITURA => ABERTURA DE TELAS
            POST => GRAVAÇÃO => CADASTRO OU EDIÇÃO

            MVCPOSTACTION4 para criar actions que resgatam os dados da tela 
            Para pegar os campos da tela aqui dentro da action, uma das formas
            eh criar parametros de entrada. Um parametro de entrada para cada campo 
            que você quiser capturar o conteudo. Ele internamente utiliza o NAME(HTML)
            Ele internamente faz um REQUEST (FORM E QUERYSTRING)
            Podemos pegar quantos e quais campos da tela quisermos(não eh obrigatório pegar todos)
        * */
        [HttpPost]
        public ActionResult Cadastrar(String nome, String email, String telefone, DateTime data, Int32 sexo)
        {
            var conexao = new AGENDAEntities();

            //criamos uma variável apontando para tabela de amigos
            //fizemos um INSERT INTO via EF
            var novoAmigo = new TB_AMIGO();

            //DATA MAPPER é um nome bonito para pegar dados de um local e levar pra outro, movimentação de dados
            novoAmigo.NM_AMIGO = nome;
            novoAmigo.DS_EMAIL = email;
            novoAmigo.NR_TELEFONE = telefone;
            novoAmigo.DT_NASCIMENTO = data;
            novoAmigo.ID_SEXO = sexo;

            //Manda adicionar o registro na tabela e o salva
            conexao.TB_AMIGO.Add(novoAmigo);
            conexao.SaveChanges();

            /* * 
                Sempre que quisermos levar dados de uma tela para outra (action) pra outra, utilizar
                um recurso exclusivo do MVC chamado TEMPDATA 
                Podemos tanto armazenar dados simples (nome, email) quanto dados complexos (lista de registros
                de uma tabela). Tudo que jogarmos dentro do TEMPDATA conseguimos visualizar de qualquer local
                do projeto. São informaçoes globais!
            * */

            //Criamos uma variável dentro do TEMPDATA(NOME, CONTEÚDO)   
            TempData.Add("SUCESSO","Amigo Cadastrado com Sucesso!");

            //O Response.Redirect aqui no MVC é o RedirectToAction()
            //assim que ele cadastrar o amigo, o levamos de volta para a tela de listar
            return RedirectToAction("Listar");
        }

        /* * 
            criamos uma action post para pegar os dados da tela para editar
            1 action para abrir a tela (levar dados para tela)
            1 action para pegar os dados da tela (vai e volta)

            MVCPOSTACTION4 + tab + tab já monta uma action POST 

            Podemos pegar os dados dos campos da tela de 2 formas
            NAME >>   CAMPO A CAMPO (CADASTRAR)
            FORMCOLLECTION >> Ele sobe o conteúdo de todos os campos de uma única vez. 
            Mais inteligente, mais prático e mais fácil
         * */
        [HttpPost]
        public ActionResult Atualizar(FormCollection dadosTela)
        {
            //Temos que pegar os dados que vieram da tela dentro de formcollection
            //(coleção de campos) e enviar para tela 

            //criamos uma variavel de conexao com o banco de dados através do EF
            var conexao = new AGENDAEntities();
            
            //DATA MAPPER (movimentacao/MAPEAMENTO de dados)
            //cuidado com o formcollection, todos os dados que vem dentro dele vem em formato de "STRING"
            //CONVERT (INT32, DATETIME)
            var codigo = Convert.ToInt32(dadosTela["codigo"]);

            //SELECT * FROM TB_AMIGO WHERE ID_AMIGO = CODIGODATELA
            var amigo = conexao.TB_AMIGO.Find(codigo);

            //fizemos o data mapper dos dados
            amigo.NM_AMIGO = dadosTela["nome"];
            amigo.DS_EMAIL = dadosTela["email"];
            amigo.NR_TELEFONE = dadosTela["telefone"];
            amigo.DT_NASCIMENTO = Convert.ToDateTime(dadosTela["data"]);
            amigo.ID_SEXO = Convert.ToInt32(dadosTela["sexo"]);

            //fizemos o update do registro
            conexao.SaveChanges();

            //informações de uma tela pra outra (action para outra)
            //madar sempre no TEMPDATA
            TempData.Add("SUCESSO", "Amigo Atualizado com Sucesso!");

            return RedirectToAction("Listar");
        }

    }
}