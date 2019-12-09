using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.CodeFirst.DataAccess;
using LGroup.CodeFirst.Model;
using NUnit.Framework;
using LGroup.CodeFirst.Repository;
using LGroup.CodeFirst.Repository.Contracts;

namespace LGroup.CodeFirst.Test.Repository
{
    [TestFixture]
    public sealed class ClienteRepositoryTest
    {
        [Test]
        public void TESTAR_ATUALIZACAO_DE_CLIENTE()
        {
            var repositorio = new ClienteRepository(new Conexao());
            var cliente = repositorio.Listar().First();

            cliente.Nome = "Nome 11:54";
            cliente.Email = "Email 11:54";

            repositorio.Atualizar(cliente);
        }
    }
}
