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
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private Conexao _conexao = new Conexao();
        public IEnumerable<ItemPedidoModel> Listar()
        {
            return _conexao.ItensPedido.ToList();
        }

        public IEnumerable<ItemPedidoModel> Pesquisar(Expression<Func<ItemPedidoModel, bool>> Filtro)
        {
            return _conexao.ItensPedido.Where(Filtro);
        }
    }
}
