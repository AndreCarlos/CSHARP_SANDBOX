using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.NullObject.Business
{
    public sealed  class DescontoNatalBusiness : Base.IDesconto
    {
        public decimal Calcular(decimal valorProduto_)
        {
            return valorProduto_ - 20;
        }
    }
}
