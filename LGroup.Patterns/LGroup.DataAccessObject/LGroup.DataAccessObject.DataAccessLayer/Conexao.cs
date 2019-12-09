using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///SUBIMOS O ENTITY FRAMEWORK PARA MEMORIA
using System.Data.Entity;
using LGroup.DataAccessObject.Model;
using System.Diagnostics;

///O que tem de mais top desde 2008 na plataforma .Net é o entity framework EF, versao atual 6.1
///O EF é uma ferramenta de mapeamentos de banco de dados
///o EF é uma ferramenta representante
///As tabelas sao representadas em formas de classes 
///Tecnologia de acesso a dados da Microsoft é o ADO.NET
///sÃO 3 categorias de manipulaçao de dados
///Modelo CONECTADO
///SqlConnection, SqlCommand, SqlDataReader, SqlDataAdapter
///
///Modelo DESCONECTADO
///DataSet, DataTable
///
///Ferramentas ORM
///LINQ TO SQL (*.DBML)
///ENTITY FRAMEWORK(*.EDMX)
///
namespace LGroup.DataAccessObject.DataAccessLayer
{
    /// <summary>
    /// transformamos a nossa classe normal em uma classe de conexao
    /// </summary>
    public sealed class Conexao : DbContext
    {
        /// <summary>
        /// Pesquisar WEB CONFIG TRANSLATION
        /// </summary>
        /// 

        //private void PegarLog(log)
        //{
        //    Debug.WriteLine(log);
        //}
        public Conexao() : base("Data Source=LGROUP05\\SQLEXPRESS; Initial Catalog=SISAMIGOS; Integrated Security=True;")
        {
            ///Para retirar as 3 queries iniciais
            Database.SetInitializer(new NullDatabaseInitializer<Conexao>());


            ///a partir do EF6 conseguimos monitorar o log
            ///tudo que vai pro banco em forma de query, dados
            ///pra tirar o log criamos um metodo anônimo (metodo inline)
            Database.Log = (log) => Debug.WriteLine(log);
        }

        ///Os mappings é para create table (mapear tabelas e campos) para fazer CRUD
        ///é a classe DBSET
        ///
        public DbSet<AmigoModel> Amigo { get; set; }
        public DbSet<SexoModel> Sexo { get; set; }
        public DbSet<EstadoCivilModel> EstadoCivil { get; set; }


        ///Apos montar os mapeamentos (configurações) das tabelas
        ///as colocamos dentro da conexao
        ///amarramos a conexao com as tabelas (mapeamento)
        ///
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ///Quando o EF for gerar ou se conectar no banco de dados
            ///Ele cria, mapeia as tabelas abaixo (mapeamento)
            /// A ordem das tabelas tanto faz (primeiro ele dá create table e depois vem dando alter table colocando
            /// as chaves estrangeiras)
            modelBuilder.Configurations.Add(new Mappings.AmigoMapping());
            modelBuilder.Configurations.Add(new Mappings.SexoMapping());
            modelBuilder.Configurations.Add(new Mappings.EstadoCivilMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
