using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//subimos as views para memória (interface)
using LGroup.MVP.View.Modules.Clientes;
using LGroup.MVP.Presenter.Modules.Clientes;

namespace LGroup.MVP.Presentation.Modules.Clientes
{
    public partial class Cadastrar : System.Web.UI.Page, ICadastrarView
    {
        ///A tela manipula o presenter (programação da tela)
        private CadastrarPresenter _presenter;


        /// <summary>
        /// A tela herda da view (propriedades = campos da tela)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///No load da tela (após carregar a tela)
            ///amarramos o presenter com a view (Interface)
            _presenter = new CadastrarPresenter(this);
        }
        
        public string Nome
        {
            get
            {
                return txtnome.Text;
            }
            set
            {
                txtnome.Text = value;
            }
        }

        public string Email
        {
            get
            {
                return txtemail.Text;
            }
            set
            {
                txtemail.Text = value;
            }
        }

        public string Telefone
        {
            get
            {
                return txttelefone.Text;
            }
            set
            {
                txttelefone.Text = value;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            _presenter.Limpar();
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            _presenter.Cadastrar();
        }
        public void ExibirMensagem(string texto_)
        {
            Response.Write("<script>alert(' "+ texto_ +"');</script>");
        }
    }
}