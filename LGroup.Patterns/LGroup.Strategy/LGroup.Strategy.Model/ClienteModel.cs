using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.Strategy.Model
{
    public sealed class ClienteModel
    {
        public Int32 Codigo { get; set; }

        public string  Nome { get; set; }

        public string  RG { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}