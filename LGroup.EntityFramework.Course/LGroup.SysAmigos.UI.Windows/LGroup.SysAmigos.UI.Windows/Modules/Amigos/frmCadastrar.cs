using System;
using System.Text;
using System.Windows.Forms;

//podemos "tomar" dois tipos de erros
//erros no modelo fisico (banco de dados)
//erros no modelo conceitual (mapeamento de classes)
//para "pegar" erros de banco de dados temos que utilizar a classe dbUpdateExeption
using System.Data.Entity.Infrastructure;

//para pegar erros no modelo conceitual - memoria
//dentro das classes do EF, mapeamento, temos que importar a namespace abaixo
//tamanho de campos, consistencia de campos - integridade da informação
//normalmente ele verifica na memoria
using System.Data.Entity.Validation;

namespace LGroup.SysAmigos.UI.Windows.Modules.Amigos
{
    public partial class frmCadastrar : Form
    {
        public frmCadastrar()
        {
            InitializeComponent();
        }

        private void btnTabela_Click(object sender, EventArgs e)
        {
            //Desde o framework 5 temos o recurso de diagramas
            //diagramas sao formas visuais (somente estetica) de agrupar as tabelas (classes)
            //fica inclusive mais rapido varios diagramas do que somente 1 diagrama com varias tabelas

            //o EF faz o tracking dos registros (monitoramento dos registros)
            //ele sabe quais registros vao ser inseridos, deletados e alterados
            //e quais vao ser selecionados, ele fica monitorando os registros

            // o edmx nada mais eh do que um XML gigantesco dividido em 3 secçoes
            //ssdl - banco
            //csdl - conceitual (classes)
            //msl - mapeamento
            //para nao ter que ficar olhando XML existe o Entity Framework Designer
            //eh a ferramenta grafica de visualizaçao do XML
            // o EF possui 3 estratégias (formas de mapeamento de banco)
            //ESTRATEGIA DATABASE FIRST  - primeiro montamos o banco de dados para depois fazer o mapeamento
            //via EF

            //Temos que criar uma variável apontando para o banco de dados
            //referenciando o EF
            //a classe de conexao do EF segue um padrao de nomenclatura 
            //NOMEDOBANCOEntities
            //LGROUPEntities
            //MICROSOFTEntities

            var conexao = new EFEntities();

            //um recurso legal, ajuda muito a entender os bastidores e funcionamnto do EF eh visualizar o output
            //tudo que ele vai fazendo, ele vai gerando o log
            //criamos um metodo anonimo, in line, para extraçao do log
            //public void CapturarLog(String comandos)
            conexao.Database.Log = (String comandos) =>
                {
                    //quando escrevemos algo dentro do console, ele escreve na janela de output
                    Console.WriteLine(comandos);
                };

            //apos criar uma variavel apontando para o EF(ORM) temos que pegar os dados da tela e levar 
            //para o banco (persistencia)

            //subimos pra memoria a classe que esta mapeaando a tabela amigo
            var novoAmigo = new TB_AMIGO();

            //o EF trabalha com estados dos registros(ciclo de vida), ele fica monitorando desde que 
            //o registro nasce ate o momento que ele morre (tracking)

            //assim que declaramos uma variavel o estado dela eh detached
            //ta na memoria e nao foi pro banco (nao esta salvo na tabela)
            var status01 = conexao.Entry(novoAmigo).State;

            //o ato de pegar os dados de um local e levar para outro local chama-se DATA MAPER

            //vazio eh diferente de null
            //para o EF reclamar que o campo esta em branco, tem que ser NULL
            if(txtNome.Text != String.Empty)
               novoAmigo.NM_AMIGO = txtNome.Text;

            if(txtEmail.Text != String.Empty)
               novoAmigo.DS_EMAIL = txtEmail.Text;

            novoAmigo.NR_TELEFONE = mskTelefone.Text;
            novoAmigo.DT_NASCIMENTO = dtpNascimento.Value;
            novoAmigo.ID_SEXO = 2;

           //apos fazer o de-para (MAPER) temos que levar este registro para o banco, temos que fazer 
            //um INSETO INTO
            //o add nao leva para o banco, o add marca o registro para gravaçao. Marcou o registro para 
            //inclusao
            conexao.TB_AMIGO.Add(novoAmigo);

            //assim que damos um add o estado do registro mudou para ADD
            //significa que quando chamarmos o savechanges esse registro vai receber um insert (ele vai
            //pra tabela)
            var status02 = conexao.Entry(novoAmigo).State;

            //eh nesse momento que levamos o registro pro banco, significa que agora vai....
            //boas praticas de gerenciamento de erro: so monitorar com try catch
            //as linhas que podem dar erro, como essa eh a unica linha que pode dar erro
            //porque nesse momento que o EF vai no banco e com certeza em algum momento var dar erro

            try
            {
                conexao.SaveChanges();

                //assim que inserimos um registro o SaveChanges() mudou o estado do registro para UNCHANGED
                //significa que o registro esta na memoria e esta no banco, esta sincronizado e nao foi
                //alterado
                var status03 = conexao.Entry(novoAmigo).State;

                //exibimos uma mensagem com o codigo daquele amigo
                MessageBox.Show("Amigo Cadastrado com Sucesso: ID > " + novoAmigo.ID_AMIGO);
            }
            //nao adianta colocar exception para pegar erro de EF
            //numca vai visualizar o erro original (ele manda olhar o Inner Exception) 
            //para pegar os erros originais que vieram do banco, temos que utilizar a classe DBUpdateException
            catch (DbUpdateException error)
            {
                //inner exception eh um erro dentro do outro
                //deu erro no banco com o EF nao conseguiu tratar e veio dando varios erros ate chegar na tela (UI)
                MessageBox.Show(error.InnerException.InnerException.Message);
            }
            //pra pegar erros no modelo conceitual (CSDL) integridade de campos 
           //pra pegar erros de tamanho de campos ou campo em branco
            catch (DbEntityValidationException error)
            {
                //esse tipo de erro eh mais complexo de capturar, temos que fazer 2 loops, um emcima do erro
                //ele grava as mensagens de forma organizada: 
                //nome da tabela - erros da tabela
                //nome da tabela - erros da tabela
                //nome da tabela - erros da tabela
                
                //primeiro loop eh pra descobrir o nome das tabelas
                //a propriedade EntityValidationErros retorna somente as tabelas que possuem erros
                //as que nao possuem erros associados não sao monitorados
                foreach (var tabelaErro in error.EntityValidationErrors)
                {
                    //armazenamos o nome da tabela que deu erro, no nosso caso
                    //so temos uma tabela -> tb_amigo
                    var nomeTabela = tabelaErro.Entry.Entity.ToString();

                    //apos descobrir a tabela, percorremos o erro dela
                    //capturamos os erros de validacao que estao associadas a tabela que deu erro no foreach acima
                    foreach (var erroValidacao in tabelaErro.ValidationErrors)
                    {
                        var mensagem =  "Tabela: " + nomeTabela + "\n" +
                                        "Campo: " + erroValidacao.PropertyName + "\n" +
                                        "Erro: " + erroValidacao.ErrorMessage;
                        
                        MessageBox.Show(mensagem);
                    }
                }
            }

            //CONSIDERAÇÕES:
            //o EF abre e fecha sozinho a conexao
            //nao precisa escrever begin, rollback, commit, ele automaticamnte abre e fecha a conexao
            //apos inserir o registro ele pega automaticamente o id(codigo)
            //o EF tem seus proprios recursos de auto performance. Ele so abre a conexao realmente quando precisa
            //ele soh abre a conexao e a transacao e vai pro banco quando executamos o comando SAVECHANGES
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            //da mesma forma que mapeamos tabela => classes
            //temos que mapear procedures (metodos) inclusive eles nao ficam visiveis
            //no diagrama. Para enxergar as precedures temos que abrir a janela Model Browser
            var conexao = new EFEntities();
            conexao.Database.Log = (comandos) => Console.WriteLine(comandos);

            //pegamos os campos da tela e passamos para a procedure
            //se voce mapeou algum campo que eh um tipo de valor (value type) = struct
            //int32, datetime, boolean, decimal = nullable(pode ficar em branco)
            //eh no momento que chamamos a procedure que abrimos a conexao
            conexao.CADASTRAR_AMIGOS(
                    txtNome.Text,
                    txtEmail.Text,
                    mskTelefone.Text,
                    dtpNascimento.Value,
                    1
                );
            MessageBox.Show("Amigo Cadastrado com Sucesso!");
        }

        private void btnTSQL_Click(object sender, EventArgs e)
        {
            //caso seja preciso voltar no tempo e montar uma string sql na unha,
            //podemos utilizar o EF 
            var conexao = new EFEntities();
            conexao.Database.Log = (comandos) => Console.WriteLine(comandos);
            
            //montamos a string de insert (gambi) o certo mesmo eh usar os mapeamentos 
            var tsql = new StringBuilder();
            tsql.AppendLine("INSERT INTO TB_AMIGO(NM_AMIGO,DS_EMAIL, ");
            tsql.AppendLine("NR_TELEFONE,DT_NASCIMENTO,ID_SEXO) ");
            tsql.AppendLine("VALUES ('" + txtNome.Text + "','" + txtEmail.Text + "','" + mskTelefone.Text + "','" + dtpNascimento.Value.ToString("yyyy-MM-dd") + "', 2)");

            //quando montamos um sql na mão, chama-se um comando adhoc, e mandamos levar para o banco via EF através
            //do comando ExecuteSqlCommand
            conexao.Database.ExecuteSqlCommand(tsql.ToString());

            MessageBox.Show("Amigo Cadastrado com Sucesso!");
        }

        private void btnTransacional_Click(object sender, EventArgs e)
        {
            //da mesma forma que no SQL ou via adhoc trabalhamos com controle transacional 
            //begin transaction, commit transactiion, rollback transaction tambem podemos utilizar
            //esses recursos de transaçoes (confirmacao de execucao) no EF
            var conexao = new EFEntities();
            conexao.Database.Log = (comandos) => Console.WriteLine(comandos);

            //nao precisa abrir transacao manualmente, o EF ja faz isso
            // mas caso precise, segue o codigo abaixo

            //CONSIDERAÇÕES:
            //1- quando abrimos manualmente uma transacao eh nesse momento que ele abre a conexao com o banco de dados e também abre um begin transaction
            var transacao = conexao.Database.BeginTransaction();
            var novoAmigo = new TB_AMIGO();

            novoAmigo.NM_AMIGO = txtNome.Text;
            novoAmigo.DS_EMAIL = txtEmail.Text;
            novoAmigo.NR_TELEFONE = mskTelefone.Text;
            novoAmigo.DT_NASCIMENTO = dtpNascimento.Value;
            novoAmigo.ID_SEXO = 2;

            conexao.TB_AMIGO.Add(novoAmigo);
            conexao.SaveChanges();

            //confirmamos a transacao, tudo que foi feito será inserido na tabela
            //no final de uma transacao, temos que mandar um dos dois comandos
            //o commit -> confirma a transacao
            //o rollback -> ignora a transacao (descarta o que fizemos na transaçao)
            transacao.Rollback();
        }

        //Desde o C#5.0 (VS 2012) podemnos abrir threads utilizando ASYNC e AWAIT
        //New Thread(), New Task() == até o VS2010
        //ASYNC trabalha em conjunto com o AWAIT
        //ASYNC significa que algum coidgo aqui dentro do metodo vai ser executado
        //dentro de uma THREAD (SaveChangesAsync)
        //WAIT significa aguarde o termino da THREAD, ele fica esperando o termino do INSERT
        //para soh depois dar o messagebox
        private async void CadastrarAsync()
        {
            var conexao = new EFEntities();
            conexao.Database.Log = (comandos) =>
                {
                    Console.WriteLine(comandos);
                };
            var novoAmigo = new TB_AMIGO();

            novoAmigo.NM_AMIGO = txtNome.Text;
            novoAmigo.DS_EMAIL = txtEmail.Text;
            novoAmigo.NR_TELEFONE = mskTelefone.Text;
            novoAmigo.DT_NASCIMENTO = dtpNascimento.Value;
            novoAmigo.ID_SEXO = 2;

            conexao.TB_AMIGO.Add(novoAmigo);

            //novidade do EF 6.0, agora podemos fazer consultas e inserts de forma assincrona,
            //ele intermanente agra um trhead e exeuta aquele insert dentro de uma thread (performance)
            await conexao.SaveChangesAsync();

            MessageBox.Show("Amigo Cadastrado com Sucesso");
        }

        private void btnAssincrono_Click(object sender, EventArgs e)
        {
            //o processador eh divido em:
            //hardware (processador) -> nucleos -> threads(trilhas de execução)
            //existem 2 tipos de processamento:
            //processamento sincrono(padrao) - roda uma linha por vez, um codigo a cada vez, soh vai rodar o de baixo 
            //quando termina de rodar o de cima

            //processamento assincrono:
            //conseguimos rodar varias linhas de codigo ao mesmo tempo. Cada bloco de codigo ele roda 
            //em uma thread (trilha) = processador

            CadastrarAsync();

            var migue = "Zina";
        }
    }
}
