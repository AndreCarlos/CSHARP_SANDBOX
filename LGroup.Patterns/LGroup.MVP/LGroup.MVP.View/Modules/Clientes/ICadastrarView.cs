using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.MVP.View.Modules.Clientes
{
    /// <summary>
    /// Quando implementamos o padrão MVP, podemos implementar de 2 formas
    /// inclusive cada tela de uma forma diferente 
    /// 1ª PASSIVE VIEW (view passiva)
    /// A view (interface) não manipula o model (CADASTRAR)
    /// 
    /// 2ª SUPERVISING CONTROLLER (Controller supervisionado)
    /// se a view (interface) manipular o model (LISTAR)
    /// 
    /// </summary>
    public interface ICadastrarView
    {
        /// <summary>
        /// Para cada campo da tela, uma propriedade
        /// </summary>
        string Nome { get; set; }
        string Email { get; set; }
        string Telefone { get; set; }


        void ExibirMensagem(string texto_);
    }
}
