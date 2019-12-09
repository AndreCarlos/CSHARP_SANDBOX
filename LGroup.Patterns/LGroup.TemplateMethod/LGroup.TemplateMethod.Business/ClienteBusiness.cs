using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.TemplateMethod.Model;

namespace LGroup.TemplateMethod.Business
{
    public sealed class ClienteBusiness : Base.BaseBusiness<ClienteModel>
    {
        protected override bool ValidarCamposObrigatorios(ClienteModel modelo_)
        {
            var retorno = true;

            if (modelo_.Nome == null)
                retorno = false;
            else if (modelo_.Telefone == null)
                retorno = false;

            return retorno;
        }

        protected override void Cadastrar(ClienteModel modelo_)
        {
            //fake cadastrou...
        }
    }
}
