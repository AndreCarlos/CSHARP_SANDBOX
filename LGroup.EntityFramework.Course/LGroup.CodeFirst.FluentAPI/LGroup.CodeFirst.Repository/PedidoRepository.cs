using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//dados
using LGroup.CodeFirst.Model;

//conexao
using LGroup.CodeFirst.DataAccess;

//definicao de comandos crud
using LGroup.CodeFirst.Repository.Contracts;

//lambdas
using System.Linq.Expressions;

//para poder alterar o estado dos registros, importamos essa namespace do EF
using System.Data.Entity;

namespace LGroup.CodeFirst.Repository
{
    /* falando de repositorios
     *  na interface - padronizamos, definimos
     *  na classe - programamos, fazemos o CRUD
     */

    public class PedidoRepository : IPedidoRepository
    {
        private Conexao _conexao = new Conexao();
        public IEnumerable<PedidoModel> Listar()
        {
            return _conexao.Pedidos.ToList();
        }

        public IEnumerable<PedidoModel> Pesquisar(Expression<Func<PedidoModel, bool>> Filtro)
        {
            return _conexao.Pedidos.Where(Filtro);
        }

        public void Cadastrar(PedidoModel dadosTela)
        {
            _conexao.Pedidos.Add(dadosTela);
            _conexao.SaveChanges();
        }

        public void Atualizar(PedidoModel dadosTela)
        {
            //pra fazer um update no registro, coloca-lo na conexao atraves do comando
            //Entry e mudar o status dele para MODIFIED
            _conexao.Entry(dadosTela).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            var pedido = _conexao.Pedidos.Find(codigo);
            _conexao.Pedidos.Remove(pedido);
            _conexao.SaveChanges();
        }
    }
}
