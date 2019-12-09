using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Adapter.Model;
using LGroup.Adapter.Business;
using NUnit.Framework;

namespace LGroup.Adapter.Test
{
    /// <summary>
    /// testamos os boletos normais e os externos (adaptadores)
    /// </summary>
    /// 
    [TestFixture]
    public sealed class BoletoTest
    {
        [Test]
        public void Testar_Boleto_Hsbc()
        {
            ///boleto normal (nacional)
            ///
            var boleto = new BoletoModel();
            boleto.DataVencimento = DateTime.Now;
            boleto.Cedente = "Net Coders";
            boleto.Sacado = "Voce";
            boleto.Valor = 1000;

            var negocioBoleto = new HSBCBusiness();
            negocioBoleto.EmitirBoleto(boleto);
        }

        [Test]
        public void Testar_Boleto_BOA()
        {
            var boleto = new BoletoModel();
            boleto.DataVencimento = DateTime.Now;
            boleto.Cedente = "Net Coders";
            boleto.Sacado = "Voce";
            boleto.Valor = 1000;


            ///para quem chama é imperceptivel
            ///nao dá para saber se é uma classe nacional ou estrangeira 
            ///essa classe abaixo é a classe adaptadora
            ///ela pega os dados do boletomodel e transmite, coloca no formato para repassar para 
            ///classe BILLET (TERCEIRO)
            ///Uma classe mascarando a classe verdadeira
            var negocioBoleto = new BankOfAmericaBusiness();
            negocioBoleto.EmitirBoleto(boleto);
        }
    }
}
