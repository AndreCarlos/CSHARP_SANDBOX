using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.MVP.View.Modules.Clientes;
using LGroup.MVP.Model;

namespace LGroup.MVP.Presenter.Modules.Clientes
{
    public sealed class ListarPresenter
    {
        private readonly IListarView _view;

        /// <summary>
        /// Para o presenter visualizar (manipular) os dados da tela, utilizamos
        /// a view (interface) e vice-versa
        /// </summary>
        public ListarPresenter(IListarView view_)
        {
            _view = view_;
        }

        public void Carregar()
        {
            ///Se fosse  um cenário REAL, o presenter chamaria a DAO
            ///Preenchemos uma lista de clientes e descemos para tela
            var clientes = new List<ClienteModel>
            {
                new ClienteModel { Codigo = 1, Nome = "Leo", Email = "teste@lgroup.com", Telefone = "011 8447827429"}
            };

            ///Jogamos a lista para dentro da view
            ///jogou na view automaticamente vai para tela
            _view.Clientes = clientes;
        }
    }
}
