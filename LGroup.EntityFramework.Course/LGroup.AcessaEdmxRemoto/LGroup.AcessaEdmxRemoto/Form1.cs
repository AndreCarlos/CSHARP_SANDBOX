using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//subimos para memoria todas as referencias que desceram do serviço
//Proxy eh um nome bonitao que utilizamos quando adicionamos um DLL Remota
//Demos um alias (apelido) para a namespace, muito util quando temos o mesmo nome de classe
//descendo de várias DLLs, namespace (ambiguo = vermelho)
using PROXY = LGroup.AcessaEdmxRemoto.ProxyOData;

namespace LGroup.AcessaEdmxRemoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //o código tá local, soh que o EDMX, tables, estão remotas, o processamento eh remoto
            var url = new Uri("http://localhost:3322/oData");

            //Essa classe container equivale a classe Entitites(eh a classe de conexao)
            var conexao = new PROXY.Container(url);
            
            //demos um select remoto (na nuvem) (oData)
            dataGridView1.DataSource = conexao.TB_PRODUTO.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //o código tá local, soh que o EDMX, tables, estão remotas, o processamento eh remoto
            var url = new Uri("http://localhost:3322/oData");

            //Essa classe container equivale a classe Entitites(eh a classe de conexao)
            var conexao = new PROXY.Container(url);

            var novoProduto = new PROXY.TB_PRODUTO();
            novoProduto.NM_PRODUTO = "Produto das 12:36";
            novoProduto.NM_DESCRICAO = "Descricao do produto";
            novoProduto.FLG_STATUS = true;
            novoProduto.NM_CATEGORIA = "Móveis";
            novoProduto.NM_FORNECEDOR = "Marabras";
            novoProduto.VL_PRODUTO = 99;

            //Se fosse o EDMX local (Tb_Produto.Add())
            //Se fosse um EDMX remoto (oData) da forma abaixo
            conexao.AddToTB_PRODUTO(novoProduto);
            conexao.SaveChanges();
        }
    }
}
