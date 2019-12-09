using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SisClientes.DataTransferObject
{
    public sealed class ClienteDTO
    {
        public Int32 Codigo { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String Telefone { get; set; }
    }
}
