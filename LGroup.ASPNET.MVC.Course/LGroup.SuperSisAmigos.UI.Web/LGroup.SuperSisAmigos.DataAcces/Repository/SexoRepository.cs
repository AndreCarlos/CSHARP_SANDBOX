using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///importamos as entidades de domínio(classes que armazenam dados)
using LGroup.SuperSisAmigos.Entities;

///importamos os serviços de dominio (comandos das classes que armazenam dados) (tabelas)
using LGroup.SuperSisAmigos.Entities.Repository;

namespace LGroup.SuperSisAmigos.DataAcces.Repository
{
    /// <summary>
    /// É no repositório que fica o CRUD
    /// É no repositório que abrimos a conexão e fazemos os comandos de banco
    /// As classes de repository tem que herdar das interfaces de repository
    /// </summary>
    public sealed class SexoRepository : ISexoRepository
    {
        /// <summary>
        /// Criamos uma variável apontando para classe de conexão (DbContext)
        /// </summary>
        /// <returns></returns>

        private Conexao _conexao = new Conexao();
        public IEnumerable<Sexo> Listar()
        {
            ///Demos um SELECT * FROM TB_SEXO através do EF
            return _conexao.Sexos.ToList();
        }

        public Sexo Pesquisar(int codigoRegistro)
        {
            ///Denos um SELECT * FROM TB_SEXO WHERE ID_SEXO = codigoRegistro
            return _conexao.Sexos.Find(codigoRegistro);
        }
    }
}
