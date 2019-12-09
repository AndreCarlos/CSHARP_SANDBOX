using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business.Contexts
{
    public sealed class DescontoContext
    {
        private IDescontoStrategy _estrategia;
        public DescontoContext(IDescontoStrategy estrategia_)
        {
            _estrategia = estrategia_;
        }

        public Decimal Calcular(Decimal valorProduto_)
        {
            return _estrategia.CalcularDesconto(valorProduto_);
        }
    }
}
