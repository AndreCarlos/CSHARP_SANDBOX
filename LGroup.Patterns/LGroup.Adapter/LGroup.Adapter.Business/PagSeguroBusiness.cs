using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Adapter.Model;

///subimos para memoria a dll externa (de terceiros)
using UOL;

namespace LGroup.Adapter.Business
{
    /// <summary>
    /// classe adaptadora
    /// para cada 1 dll ==> uma classe adaptadora
    /// </summary>
    /// 
    ///existem duas formas de fazer adaptação: 
    ///1ª forma -- composiçao (new) (classe adaptadora dando new na classe verdadeira)
    ///2ª forma -- herança (classe adaptadora herda da classe externa = verdadeira)
    ///

    /// quando uma classe herda de uma e somente de uma classe, e várias interfaces
    /// a classe sempre tem que vir antes da interface 
    public sealed class PagSeguroBusiness :  PagSeguro, Base.IBandeiraBusiness
    {
        public void Pagar(PagamentoModel pagamento_)
        {
            ///adaptado (entrou pagamento e saiu decimal e string)
            Debitar(pagamento_.Valor, pagamento_.Senha);
        }
    }
}
