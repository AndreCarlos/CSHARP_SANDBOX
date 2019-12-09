using System;
using System.Collections.Generic;

//VERSÃO MÉTODOS DE EXTENSÃO...

//quando efetuamos consulta dentro do EF(where, orderby, max, select) variacoes de TSQL
//o nome técnico eh LINQ TO ENTITIES
//todos os comandos prontos de consulta da plataforma .NET vem da namespace abaixo
//quando efeturamos consultas LINQ TO ENTITIES, podemos fazer de duas formas 
//1- Métodos de extensao => pronto pela Microsoft 
//2- Operadores de consulta => na raça, montagem da consulta ad-hoc
using System.Linq;

using System.Windows.Forms;

//Uma forma mais elegante de monitorar a app, de saber todos os locais que voce foi passando desde que deu Play
//na app e atraves da janela IntelliTrace
//ela eh um historico de acontecimentos da app, e inclusive monitora o EF
//tem desde o VS2010 soment nas ediçoes Premium e Ultimate
namespace LGroup.SysAmigos.UI.Windows.Modules.Amigos
{
    public partial class frmListar : Form
    {
        //criamos um objeto apontando para classe de conexao(contexto) do EF
        //declaramos aqui para ficar visivel em toda a tela
        private EFEntities _conexao = new EFEntities();

        public frmListar()
        {
            InitializeComponent();
        }

        private void frmListar_Load(object sender, EventArgs e)
        {
            //habilitamos o log para ver todas as queries
            _conexao.Database.Log = (comandos) => Console.WriteLine(comandos);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SELECT SECO
            //demos um select * from e jogamos para o grid
            //a acao de pagar dados e amarrar, vincular dentro de um combo, listbox
            //grid se chama DATA BINDING (vinculacao de dados)
            //quando efetuamos consulta (select, where, order by, union, group by) sempre
            //colocar o ToList() no final da consulta, eh ele quem abre a conexao e envia os 
            //dados para o banco. O ToList eh o disparador
            dgvAmigos.DataSource = _conexao.TB_AMIGO.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SELECT ALGUNS CAMPOS
            //quando damos um new sem passar o nome de uma classe, se chama TIPO ANÔNIMO
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Select(x => new { x.ID_AMIGO, x.NM_AMIGO, x.DS_EMAIL}).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SELECT COM ALIAS
            //os alias seguem as mesmas convencoes de uma variavel
            //nao pode começar com numeros, nao podem ter espaco, nao podem ter caracteres especiais
            //nao pode por entre aspas duplas
            //os alias nao vem do banco, o EF coloca na memoria 
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Select(x => new { 
                                                            Nome = x.NM_AMIGO, //propriedades vem na frente(ALIAS)
                                                            Telefone =x.NR_TELEFONE, 
                                                            Email = x.DS_EMAIL }).ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //WHERE SIMPLES
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where(x => x.ID_SEXO == 2).ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //WHERE COMPLEXO
            // 1ª otimizacao
            //sempre que for fazer um filtro usando duas vezes o mesmo campo
            //id_sexo == 1 || ou id_sexo == 2 pra que a query vire um IN colocar entre parenteses o campo repetido
            //2ª otimizacao
            //sempre que for fazer um filtro de data, deixar a data em uma variavel. Se deixar a data direto dentro
            //do where, o EF fará um convert da data
            var data = new DateTime(1930, 01, 01);
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where( x => x.ID_AMIGO>=1 && (x.ID_SEXO == 1 || x.ID_SEXO == 2) &&
                                                            x.DT_NASCIMENTO >= data).ToList(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ORDER BY 1 CAMPO ASC
            dgvAmigos.DataSource = _conexao.TB_AMIGO.OrderBy(x => x.NM_AMIGO).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ORDER BY VARIOS CAMPOS ASC
            //variacao legal
            //dgvAmigos.DataSource = _conexao.TB_AMIGO.OrderBy(x => new { 
            //                                                    x.NM_AMIGO,
            //                                                    x.NR_TELEFONE}).ToList();

            //variacao ruim
            //em versoes antigas funcionava ou sem update ou vs 2012
            dgvAmigos.DataSource = _conexao.TB_AMIGO.OrderBy(x => x.NM_AMIGO).OrderBy(x => x.NR_TELEFONE).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //ORDER BY 1 CAMPO DESC
            //quando for fazer diversas ordernacoes (por varios campos). O primeiro eh orderby ou orderbydescending
            //do segundo em diante eh tehnby(asc) ou thenbydescending(desc)
            dgvAmigos.DataSource = _conexao.TB_AMIGO.OrderByDescending(x => x.NM_AMIGO).ThenBy(x => x.NR_TELEFONE)
                                                                             .ThenByDescending(x => x.DS_EMAIL).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //INNER JOIN
            //como as tableas se relacionam no banco, quando montamos o EF
            //ele tambem montou o relacionamdnto entre as classes
            //para fazer o join chama o navigation property (tb_sexo)
            //os relacionamentos viram propriedades de navegacao
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Select(x => new
            {
                x.NM_AMIGO,
                x.ID_SEXO,
                x.TB_SEXO.DS_SEXO
            }).ToList();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //LIKE ESQUERDA 
            //startswith == comeca com 
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where(x => x.NM_AMIGO.StartsWith("f")).ToList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //LIKE DIREITA
            //endswith == termina com
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where(x => x.NM_AMIGO.EndsWith("Silva")).ToList();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //LIKE NO MEIO 
            //contains == possua, contenha
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where(x => x.NM_AMIGO.Contains("a")).ToList();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //COUNT
            MessageBox.Show(_conexao.TB_AMIGO.Count().ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //MAX
            MessageBox.Show(_conexao.TB_AMIGO.Max(x => x.ID_AMIGO ).ToString());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //MIN
            MessageBox.Show(_conexao.TB_AMIGO.Min(x => x.ID_AMIGO).ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //SUM
            MessageBox.Show(_conexao.TB_AMIGO.Sum(x => x.ID_AMIGO).ToString());
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //AVERAGE
            MessageBox.Show(_conexao.TB_AMIGO.Average(x => x.ID_AMIGO).ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //BOLADAO DE QUERIES
            //podemos colocar os comandos em qualquer ordem, where, orderby
            //desde que termine com SELECT
            dgvAmigos.DataSource = _conexao.TB_AMIGO.Where(x => x.ID_AMIGO >= 1 &&
                                                           (x.ID_SEXO == 1 || x.ID_SEXO == 2) &&
                                                           x.NM_AMIGO.Contains("a")).OrderBy(x => new
                                                           {
                                                               x.NM_AMIGO,
                                                               x.DS_EMAIL
                                                           })
                                                           .ThenByDescending(x => x.DT_NASCIMENTO)
                                                           .Select(x => new   //tem que terminar com o select, nao importa a ordem dos filtros!
                                                           {
                                                               codigo = x.ID_AMIGO,
                                                               Nome   = x.NM_AMIGO,
                                                               Sexo   = x.TB_SEXO.DS_SEXO
                                                           }).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //ACIONAMOS A PROCEDURE
            //procedures sao mais rapidas que comandos ad-hock
            //insert, delete, select, update = comandos TSQ
            //o melhos dos mundos eh EF com procedure

            dgvAmigos.DataSource = _conexao.LISTAR_AMIGOS();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //EXCLUIR 1 REGISTRO
            //armazenamos o codigo do amigo selecionado dentro do grid
            //note: ao tirar a informaçao de uma coluna fazer sempre a conversao, ele nao sabe que tem um numero la dentro
            //ele eh burro, pra ele  eh tudo Object
            var codigo = Convert.ToInt32(dgvAmigos.SelectedRows[0].Cells[0].Value);

            //EF - antes de exluir um registro temos que seleciona-lo
            //atraves do codigo, trouxemos um regitro pra memoria. Primeiro traz para memoria e depois deleta...
            //ao fazer uma consulta no bd, temos 3 comandos Linq to Entities que nos auxiliam a busca de dados
            //Where -> para trazer varios registros (nome = maria, idade >=18, uf=sp)
            //First -> para trazer somente o primeiro registro de varios utilizar o First
            //Single -> para trazer um unico registro (normalmente utilizado para ID)

            //quando executamos o comando single, ele nos obriga a ter somente 1 unico registro na tabela,
            //caso retorne mais de um registro vai dar eero, ele foi projetados para trazer 1 unico registro sempre
            //Internamente ele da um select top 2 e caso retorne 2 registros ele lança um erro
            var amigoExcluido = _conexao.TB_AMIGO.Single(x => x.ID_AMIGO == codigo);
            var status01 = _conexao.Entry(amigoExcluido).State;

            //com o registro na memoria, mandamos excluir fisicamente da tabela
            //o remove marcou o registro pra exlusao e MUDOU O STATUS
            _conexao.TB_AMIGO.Remove(amigoExcluido);
            var status02 = _conexao.Entry(amigoExcluido).State;

            //eh esse comando que abre a conexao e efetua o Delete
            //quando excluimos, o registro passa por 3 estados
            //Unchanged - ta no bd e nao foi modificado
            //Deleted - o registro foi marcado para exclusão
            //Detached - o registro ta na meomoria e nao está no banco(removido fisicamente)
            _conexao.SaveChanges();
            var status03 = _conexao.Entry(amigoExcluido).State;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //EXCLUIR VÁRIOS REGISTROS
            //Novidade do EF6.0, agora podemos remover vários registros de uma vez

            //CRIAMOS UMA LISTA (colecao) para armazenar os registros offline
            //pois mandaremos todos serem excluídos de 1 uma vez
            var listaAmigos = new List<TB_AMIGO>();

            //percorremos todos os registros selecionados dentro do grid
            for (int i = 0; i < dgvAmigos.SelectedRows.Count; i++)
            {
                var codigoAmigo = Convert.ToInt32(dgvAmigos.SelectedRows[i].Cells[0].Value);
                
                //atraves do codigo levamos o registro para memoria
                var amigoExcluido = _conexao.TB_AMIGO.Single(x => x.ID_AMIGO == codigoAmigo);

                listaAmigos.Add(amigoExcluido);
            }

            //mandamos excluir todos de uma unica vez
            _conexao.TB_AMIGO.RemoveRange(listaAmigos);
            _conexao.SaveChanges();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //EDITAR AMIGO
            //quando o usuario selecionar um amigo, temos que abrir a tela de edição
            //e mostrar os dados do amigo selecionado

            var codigo = Convert.ToInt32(dgvAmigos.SelectedRows[0].Cells[0].Value);

            //abrimos a tela de edicao e passamos o codigo(id do amigo) para tela de editar
            var telaEditar = new frmEditar();
            telaEditar.CodigoDoAmigo = codigo;

            //warning: desabilitar o tracking e verificar o CACHE
            telaEditar.ShowDialog();
        }
    }
}
