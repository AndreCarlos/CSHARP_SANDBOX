using System;
using LGroup.Adapter.Model;

namespace LGroup.Adapter.Business
{
    public sealed class HSBCBusiness : Base.IBoletoBusiness
    {
        public void EmitirBoleto(BoletoModel boleto_)
        {
            if (boleto_.Valor == 0)
                throw new ApplicationException("Valor Zerado");

            if (String.IsNullOrWhiteSpace(boleto_.Sacado))
                throw new ApplicationException("Nome do Sacado em branco");
        }
    }
}
