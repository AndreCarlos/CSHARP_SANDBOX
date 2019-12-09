using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.Adapter.Model
{
    public sealed class PagamentoModel
    {
        public Int32 Codigo { get; set; }

        public decimal Valor { get; set; }

        public string Usuario { get; set; }

        public String Senha { get; set; }
    }
}
