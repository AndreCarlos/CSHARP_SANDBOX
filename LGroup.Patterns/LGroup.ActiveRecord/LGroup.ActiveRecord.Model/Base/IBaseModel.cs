using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.ActiveRecord.Model.Base
{
    /// <summary>
    /// No super tipo deixamos os comandos do crud
    /// </summary>
    public interface IBaseModel<TModelo>
    {
        /// <summary>
        /// No AR os metodos quase sempre nao possuem parametros de entrada
        /// porque os dados ficam no proprio model
        /// </summary>
        void Cadastrar();
        void Atualizar();
        void Deletar();
        IEnumerable<TModelo> Listar();
    }
}
