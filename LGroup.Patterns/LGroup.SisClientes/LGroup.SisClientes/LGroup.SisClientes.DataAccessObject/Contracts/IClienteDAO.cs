using LGroup.SisClientes.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SisClientes.DataAccessObject.Contracts
{
    public interface IClienteDAO
    {
        IEnumerable<ClienteDTO> Listar();

        void Cadastrar(ClienteDTO novoCliente_);
    }
}
