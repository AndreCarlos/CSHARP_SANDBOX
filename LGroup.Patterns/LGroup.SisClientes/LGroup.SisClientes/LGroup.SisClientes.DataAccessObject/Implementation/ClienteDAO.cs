using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.SisClientes.DataTransferObject;

namespace LGroup.SisClientes.DataAccessObject.Implementation
{
    public sealed class ClienteDAO : Contracts.IClienteDAO
    {
        private readonly Conexao _conexao;

        /// <summary>
        /// Encontrou o bigodinho é IOC
        /// </summary>
        /// <param name="conexao_"></param>
        public ClienteDAO(Conexao conexao_)
        {
            _conexao = conexao_;
        }

        public IEnumerable<ClienteDTO> Listar()
        {
            return _conexao.Clientes.ToList();
        }

        public void Cadastrar(ClienteDTO novoCliente_)
        {
            _conexao.Clientes.Add(novoCliente_);
            _conexao.SaveChanges();
        }
    }
}
