using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Esse é o único padrao visual onde as views NÃO são telas
///as views são interfaces. Para cada view, uma interface
///
using LGroup.MVP.Model;

namespace LGroup.MVP.View.Modules.Clientes
{
    /// <summary>
    /// Como na tela tem dois campos: uma grid e um botão, mas só o grid temos que carregar
    /// criamos uma propriedade para o grid
    /// </summary>
    public interface IListarView
    {
        /// <summary>
        /// como só vamos levar dados para o grid é o set
        /// não precisamos ler o grid não precisa do get
        /// </summary>
        IEnumerable<ClienteModel> Clientes { set; }
    }
}
