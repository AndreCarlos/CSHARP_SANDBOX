using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.TemplateMethod.Model
{
    public sealed class PedidoModel
    {
        public Int32 Codigo { get; set; }

        public Int32 CodigoCliente { get; set; }

        public DateTime DataCadastro { get; set; }

        public Decimal ValorTotal { get; set; }
    }
}
