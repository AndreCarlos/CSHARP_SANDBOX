using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.NullObject.Business
{
    /// <summary>
    /// tem um padrao do Martin Fowler (2009) 
    /// NULL OBJECT
    /// Com ele criamos uma classe default (padrao) para dar new
    /// em situaçoes de new reference
    /// quando nao tiver nenhuma classe para dar new, damos um new na classe padrao
    /// classe vazia, sem nenhuma regra, inteligencia, só para nao dar errado no código
    /// </summary>
    public sealed class DescontoNullObjectBusiness : Base.IDesconto
    {
        public decimal Calcular(decimal valorProduto_)
        {
            ///para qualquer data que nao seja natal e blackfriday retorna
            ///o proprio valor do produto (nao damos desconto)
            return valorProduto_;
        }
    }
}
