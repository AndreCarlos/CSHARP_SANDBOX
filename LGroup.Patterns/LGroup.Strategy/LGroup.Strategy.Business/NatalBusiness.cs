using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business
{
    public sealed class NatalBusiness : IDescontoStrategy
    {
        public decimal CalcularDesconto(decimal valor_)
        {
            return valor_ - (valor_ * 0.2M);
        }
    }
}
