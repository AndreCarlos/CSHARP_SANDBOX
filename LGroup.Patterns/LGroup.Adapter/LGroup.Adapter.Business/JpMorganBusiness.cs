using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Adapter.Model;

using JpMorgan;

namespace LGroup.Adapter.Business
{
    /// <summary>
    /// essa eh a classe adaptadora
    /// boletomodel ==> decimal, datetime e int
    /// </summary>
    public sealed class JpMorganBusiness : Base.IBoletoBusiness
    {
        private readonly Billet _boletoJPM = new Billet();

        public void EmitirBoleto(BoletoModel boleto_)
        {
            _boletoJPM.Transmit(boleto_.Valor, boleto_.DataVencimento, boleto_.Codigo);
        }
    }
}
