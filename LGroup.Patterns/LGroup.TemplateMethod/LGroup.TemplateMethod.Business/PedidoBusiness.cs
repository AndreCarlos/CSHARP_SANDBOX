using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.TemplateMethod.Model;

namespace LGroup.TemplateMethod.Business
{
    public sealed class PedidoBusiness : Base.BaseBusiness<PedidoModel>
    {
        protected override bool ValidarCamposObrigatorios(PedidoModel modelo_)
        {
            var retorno = true;

            if (modelo_.CodigoCliente == null)
                retorno = false;
            else if (modelo_.ValorTotal == null)
                retorno = false;

            return retorno;
        }

        protected override void Cadastrar(PedidoModel modelo_)
        {
            //fake :: foi no banco de dados e fez um insert na tabela 
        }
    }
}
