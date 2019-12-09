using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Montamos uma estrategia de descontos por data festiva
///se for natal,  natal = valor - 20%
///blackfriday = (valor * 2) - 50%

namespace LGroup.Strategy
{
    public interface IDescontoStrategy
    {
        Decimal CalcularDesconto(Decimal valor_);
    }
}
