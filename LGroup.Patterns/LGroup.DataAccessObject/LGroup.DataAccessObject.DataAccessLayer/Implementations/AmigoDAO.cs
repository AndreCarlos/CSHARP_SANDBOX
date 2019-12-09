using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using LGroup.DataAccessObject.Model;
using System.Data.Entity;

namespace LGroup.DataAccessObject.DataAccessLayer.Implementations
{
    /// <summary>
    /// fazemos o CRUD de AMIGO
    /// REPOSITORY == É O MELHOR PADRAO PARA O DDD
    /// DAO == MELHOR PADRAO SEM O DDD
    /// </summary>
    public sealed class AmigoDAO : Contracts.IAmigoDAO
    {
        /// <summary>
        /// criamos uma variável de conexao com o banco de dados
        /// </summary>
        private readonly Conexao _conexao = new Conexao();


        public IEnumerable<AmigoModel> Listar()
        {
            /// select * from tb_amigo
            return _conexao.Amigo.ToList();
        }

        public IEnumerable<AmigoModel> Pesquisar(Expression<Func<AmigoModel, bool>> filtro_)
        {
            return _conexao.Amigo.Where(filtro_).ToList();
        }

        public void Cadastrar(AmigoModel modelo_)
        {
            _conexao.Amigo.Add(modelo_);
            _conexao.SaveChanges();
        }

        public void Atualizar(AmigoModel modelo_)
        {
            _conexao.Entry(modelo_).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar(int codigo_)
        {
           ///Pra não dar delete sem where, primeiro trazemos o registro
           ///O find buscar por chave primaria ID_AMIGO
           ///O find primeiro olha no cache pra depois olhar no banco
           ///
            var registro = _conexao.Amigo.Find(codigo_);

            _conexao.Amigo.Remove(registro);
            _conexao.SaveChanges();
        }
    }
}
