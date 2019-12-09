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
    public sealed class EstadoCivilRepository : IEstadoCivilRepository
    {
        private Conexao _conexao = new Conexao();
        public IEnumerable<EstadoCivil> Listar()
        {
            return _conexao.Civis.ToList();
        }

        public EstadoCivil Pesquisar(int codigoRegistro)
        {
            return _conexao.Civis.Find(codigoRegistro);
        }
    }
}
