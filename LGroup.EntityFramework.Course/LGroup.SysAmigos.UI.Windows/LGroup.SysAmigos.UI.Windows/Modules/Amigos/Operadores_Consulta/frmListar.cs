using System;
using System.Linq;
using System.Windows.Forms;


//VERSÃO COM OPERADORES DE CONSULTA...
namespace LGroup.SysAmigos.UI.Windows.Modules.Amigos.Operadores_Consulta
{
    public partial class frmListar : Form
    {
        public frmListar()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //quando efetuamos consultas LINQ TO ENTITIES atraves de operadores de consulta
            //montamos a query na raça, lembra muito T-SQL, um DBA se sente bem a vontade aqui no codigo
            var conexao = new EFEntities();
            conexao.Database.Log = (comandos) => Console.WriteLine(comandos);

            //encontrou a palavra FROM é operador de consulta
            dgvAmigos.DataSource = (from AMIGOS in conexao.TB_AMIGO
                                    join SEXOS in conexao.TB_SEXO
                                    on AMIGOS.ID_SEXO equals SEXOS.ID_SEXO
                                    where AMIGOS.ID_AMIGO >= 1 &&
                                         (AMIGOS.ID_SEXO == 1 || AMIGOS.ID_SEXO == 2) &&
                                         AMIGOS.NM_AMIGO.Contains("a")
                                    orderby AMIGOS.NM_AMIGO,
                                           AMIGOS.NR_TELEFONE descending
                                    select new
                                    {
                                        Codigo = AMIGOS.ID_AMIGO,
                                        Nome = AMIGOS.NM_AMIGO,
                                        Sexo = SEXOS.DS_SEXO
                                    }).ToList();
        }
    }
}
