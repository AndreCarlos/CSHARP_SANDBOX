using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGroup.SysAmigos.UI.Windows.Modules.Amigos
{
    public partial class frmEditar : Form
    {
        public frmEditar()
        {
            InitializeComponent();
        }

        //criamso uma variavel publica para receber o codigo que vai vir de outra tela
        public Int32 CodigoDoAmigo { get; set; }

        private void frmEditar_Load(object sender, EventArgs e)
        {
            //assim que abrir a tela pro usuario, temos que pegar o codigo do amigo
            //que veio da outra tela, ir na tabela e pegar o restante dos dados (infos)

            var conexao = new EFEntities();

            var amigo = conexao.TB_AMIGO.Find(CodigoDoAmigo);

            txtNome.Text = amigo.NM_AMIGO;
            txtEmail.Text = amigo.DS_EMAIL;
            mskTelefone.Text = amigo.NR_TELEFONE;
            dtpNascimento.Value = amigo.DT_NASCIMENTO;
        }

        private void btnTabela_Click(object sender, EventArgs e)
        {
            //ATUALIZAR TABELA 
            //temos que pegar os dados da tela e levar para tabela
            var conexao = new EFEntities();

            var amigo = conexao.TB_AMIGO.Find(CodigoDoAmigo);

            //fizemos a movimentacao de dados
            amigo.NM_AMIGO = txtNome.Text;
            amigo.DS_EMAIL = txtEmail.Text;
            amigo.NR_TELEFONE = mskTelefone.Text;
            amigo.DT_NASCIMENTO = dtpNascimento.Value;
            amigo.ID_SEXO = 1;

            //pra cadastrar era ADD
            //pra excluir era REMOVE
            //pra atulizar nao tem UPDATE, ALTER eh direto SAVECHANGES()
            conexao.SaveChanges();
        }
    }
}