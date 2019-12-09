using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Adapter.Model;

///fizemos um sistema de pagamentos por bandeiras, por operadoras
///Diferentes, montamos uma familia de operadoras (SUPER TIPO)
namespace LGroup.Adapter.Business.Base
{
    public interface IBandeiraBusiness
    {
        void Pagar(PagamentoModel pagamento_);
    }
}
