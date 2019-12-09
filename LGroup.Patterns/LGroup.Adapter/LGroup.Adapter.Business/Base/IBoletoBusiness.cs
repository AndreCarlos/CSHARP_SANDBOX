using System;
using LGroup.Adapter.Model;

///para o adapter obrigatoriamente precisamos de 1 familia
///interface pai e classe filha
namespace LGroup.Adapter.Business.Base
{
    public interface IBoletoBusiness
    {
        void EmitirBoleto(BoletoModel boleto_);
    }
}
