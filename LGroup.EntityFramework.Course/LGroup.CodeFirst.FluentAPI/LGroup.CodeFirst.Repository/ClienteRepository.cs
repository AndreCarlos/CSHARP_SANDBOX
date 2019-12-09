using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//dados
using LGroup.CodeFirst.Model;

//conexao
using LGroup.CodeFirst.DataAccess;

//definicao de comandos CRUD
using LGroup.CodeFirst.Repository.Contracts;

//lambdas
using System.Linq.Expressions;

//para poder alterar o estado dos registros, importamos essa namespace do EF
using System.Data.Entity;

namespace LGroup.CodeFirst.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //private Conexao _conexao = new Conexao();
        private Conexao _conexao;

        //a conexao vai vir injetada de fora (ninject)

        //o ninject injeta automaticamente classes. Para classes nao precisa ir no arquivo
        //do ninject. Soh temos que ir se for interface(IClienteRepository)
        public ClienteRepository(Conexao conexao_)
        {
            _conexao = conexao_;
        }

        public IEnumerable<ClienteModel> Listar()
        {
            return _conexao.Clientes.ToList();
        }

        public IEnumerable<ClienteModel> Pesquisar(Expression<Func<ClienteModel, bool>> Filtro)
        {
            return _conexao.Clientes.Where(Filtro);
        }

        public void Cadastrar(ClienteModel dadosTela)
        {
            _conexao.Clientes.Add(dadosTela);
            _conexao.SaveChanges();
        }

        public void Atualizar(ClienteModel dadosTela)
        {
            //como o EF faz o tracking (gerenciamento de estados)
            //dos registros, conseguimos saber quais campos estão sendo alterados e conseguimos
            //até capturar o conteudo do antes e depois de atualizarmos

            //pensando em auditoria de registros de dados eh legal monitorar
            //as informaçoes que estao vindo dentro dos campos
            var campoNome = _conexao.Entry(dadosTela).Property("Nome");
            var campoEmail = _conexao.Entry(dadosTela).Property("Email");

            //pra saber se um determinado campo foi ou nao foi alterado lah na tela
            //utilizamos a propriedade IsModified
            if (campoNome.IsModified)
            {
                //com o orginalvalue conseguimos capturar a informação que estava dentro do campo
                //antes do usuario alterar
                var nomeAntigo = campoNome.OriginalValue;

                //com o currentevalue conseguimos capturar a informacao corrente do novo nome
                //que o usuario preencheu lah na tela
                var novoNome = campoNome.CurrentValue;

                Console.WriteLine("Nome Antigo: " + nomeAntigo);
                Console.WriteLine("Novo Nome: " + novoNome);
            }

            if (campoEmail.IsModified)
            {
                //com o orginalvalue conseguimos capturar a informação que estava dentro do campo
                //antes do usuario alterar
                var emailAntigo = campoEmail.OriginalValue;

                //com o currentevalue conseguimos capturar a informacao corrente do novo nome
                //que o usuario preencheu lah na tela
                var novoEmail = campoEmail.CurrentValue;

                Console.WriteLine("Email Antigo: " + emailAntigo);
                Console.WriteLine("Novo Email: " + novoEmail);
            }

            _conexao.Entry(dadosTela).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            var cliente = _conexao.Clientes.Find(codigo);
            _conexao.Clientes.Remove(cliente);
            _conexao.SaveChanges();
        }
    }
}
