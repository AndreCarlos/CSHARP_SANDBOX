using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

///Após criar um proxy (referencia para um serviço)
///subimos ele para memória
using LGroup.RequestResponse.Client.ProxyLGroup;

namespace LGroup.RequestResponse.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///Listar
            ///O serviço tem um padrao de nomenclatura 
            ///NOMEDELE + CLIENT = sempre termina com client
            var servico = new ClienteServiceClient();
            ResponseDTO pedido = servico.Listar(new RequestDTO());

            ///chamamos um serviços utilizando o padrao  REQUEST - RESPONSE
            dataGridView1.DataSource = pedido.Clientes;

            ///Fechamos a conexao com o serviço
            servico.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///Cadastrar
            var servico = new ClienteServiceClient();
            
            ///Configuramos o pedido
            var pedido = new RequestDTO();
            pedido.Cliente = new ClienteDTO();
            pedido.Cliente.Nome = "Nome 01";
            pedido.Cliente.Email = "Email 01";
            pedido.Cliente.DataNascimento = DateTime.Now;

            ResponseDTO resposta = servico.Cadastrar(pedido);

            if (resposta.TipoMensagem == TipoMensagemVO.Sucesso)
                MessageBox.Show(resposta.Mensagem);

            servico.Close();
        }
    }
}

