using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using LGroup.CodeFirst.DataAccess;
using LGroup.CodeFirst.Repository.Contracts;
using LGroup.CodeFirst.Model;

namespace LGroup.CodeFirst.Repository
{
    // a tecnica de injecao de dependencia favorece a utilizacao de classes mock(classes dubles,
    //atores). Se por algum motivo a classe original estiver indisponivel, algum deletou,
    //erro de sintaxe erro de lógica
    public sealed class ClienteMockRepository : IClienteRepository
    {
        public IEnumerable<ClienteModel> Listar()
        {
          //a classe de conexao ta zuada (nao ta concectando)
            //fizemos um comando de listara duble (mocking)
            var clientes = new List<ClienteModel>
            {
                new ClienteModel
                {
                    Codigo = 1,
                    CPF = "123456",
                    DataNascimento = DateTime.Now,
                    Email = "email@email.com",
                    Endereco = "Endereco teste",
                    Nome = "Nome01"
                },
                 new ClienteModel
                {
                    Codigo = 2,
                    CPF = "654321",
                    DataNascimento = DateTime.Now,
                    Email = "email@email.com",
                    Endereco = "Endereco 02",
                    Nome = "Nome02"
                }
            };

            return clientes;
        }

        public IEnumerable<ClienteModel> Pesquisar(System.Linq.Expressions.Expression<Func<ClienteModel, bool>> Filtro)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(ClienteModel dadosTela)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(ClienteModel dadosTela)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }
    }
}
