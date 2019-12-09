using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///importamos as entidades de domínio(classes que armazenam dados)
using LGroup.SuperSisAmigos.Entities;

///importamos os serviços de dominio (comandos das classes que armazenam dados) (tabelas)
using LGroup.SuperSisAmigos.Entities.Repository;

///Para poder mudar o estado dos registros importamos essa namespace
using System.Data.Entity;

namespace LGroup.SuperSisAmigos.DataAcces.Repository
{
    public sealed class AmigoRepository : IAmigoRepository
    {
        private Conexao _conexao = new Conexao();
        public IEnumerable<Amigo> Listar()
        {
            return _conexao.Amigos.ToList();
        }

        public Amigo Pesquisar(int codigoRegistro)
        {
            return _conexao.Amigos.Find(codigoRegistro);
        }

        public void Cadastrar(Amigo novoRegistro)
        {
            _conexao.Amigos.Add(novoRegistro);
            _conexao.SaveChanges();
        }

        public void Atualizar(Amigo registroAlterado)
        {
            ///Colocamos o registro dentro da conexao e mudamos o estado (status) para modificado (modified)
            ///UPDATE TB_AMIGO SET NM_AMIGO = registroAlterado.Nome,
            ///                    DS_EMAIL = registroAlterado.Email
            ///WHERE iD_AMIGO = registroAlterado.Codigo
            _conexao.Entry(registroAlterado).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar(int codigoRegistro)
        {
            ///Para fazer uma exclusão, primeiro temos que buscar o registro
            ///buscamos, selecionamos o amigo que vamos deletar através do ID(CODIGO)
           var registroExcluido = _conexao.Amigos.Find(codigoRegistro);

            //Após selecionar o amigo mandamos remover da tabela
           _conexao.Amigos.Remove(registroExcluido);

            //COMMITA, ele salva, confirma os dados da tabela
           _conexao.SaveChanges();
        }
    }
}
