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
    public class ProdutoRepository : IProdutoRepository
    {
        private Conexao _conexao = new Conexao();
        public IEnumerable<ProdutoModel> Listar()
        {
            return _conexao.Produtos.ToList();
        }

        public IEnumerable<ProdutoModel> Pesquisar(Expression<Func<ProdutoModel, bool>> Filtro)
        {
            return _conexao.Produtos.Where(Filtro);
        }

        public void Cadastrar(ProdutoModel dadosTela)
        {
            _conexao.Produtos.Add(dadosTela);
            _conexao.SaveChanges();
        }

        public void Atualizar(ProdutoModel dadosTela)
        {
            _conexao.Entry(dadosTela).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            var produto = _conexao.Produtos.Find(codigo);
            _conexao.Produtos.Remove(produto);
            _conexao.SaveChanges();
        }
    }
}
