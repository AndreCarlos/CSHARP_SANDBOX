using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LGroup.MVP.View.Modules.Clientes;
using LGroup.MVP.Presenter.Modules.Clientes;
using LGroup.MVP.Model;

namespace LGroup.MVP.Presentation.Modules.Clientes
{
    /// <summary>
    /// TELA >> PRESENTER >> VIEW >> TELA
    /// </summary>
    public partial class Listar : System.Web.UI.Page, IListarView
    {
        private ListarPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Assim que a tela subir para memoria
            ///inicializamos o presenter (programaçao da tela)
            _presenter = new ListarPresenter(this);
        }
        public IEnumerable<ClienteModel> Clientes
        {
            set 
            { 
                ///Pegamos os dados que chegaram na propriedade
                ///e passamos para dentro do grid
                grvClientes.DataSource = value;
                grvClientes.DataBind();
            }
        }

        protected void btnCarregar_Click(object sender, EventArgs e)
        {
            _presenter.Carregar();
        }
    }
}