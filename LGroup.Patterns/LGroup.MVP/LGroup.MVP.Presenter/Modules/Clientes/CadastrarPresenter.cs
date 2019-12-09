using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///subimos para memoria a view (interface)
using LGroup.MVP.View.Modules.Clientes;
using LGroup.MVP.Model;

///O presenter a grosso modo é como se fosse um controller
///É onde fica toda a programação da tela
///Toda a inteligência da TELA

namespace LGroup.MVP.Presenter.Modules.Clientes
{
    /// <summary>
    /// A integração do presenter com a tel é feita pela view (interface)
    /// tudo que jogarmos na view tanto o presenter quanto a tela vão ler
    /// PRESENTER >> VIEW >> TELA
    /// TELA >> VIEW >> PRESENTER
    /// </summary>
    public sealed class CadastrarPresenter
    {
        private readonly ICadastrarView _view;
        public CadastrarPresenter(ICadastrarView view_)
        {
            _view = view_;
        }
        public void Limpar()
        {
            _view.Nome = string.Empty;
            _view.Email = string.Empty;
            _view.Telefone = string.Empty;
        }

        public void Cadastrar()
        {
            ///Pegamos os dados da view (Interface) e levamos para o model
            var novoCliente = new ClienteModel();
            novoCliente.Nome = _view.Nome;
            novoCliente.Email = _view.Email;
            novoCliente.Telefone = _view.Telefone;

            _view.ExibirMensagem("Cliente Cadastrado com Sucesso!");
        }
    }
}
