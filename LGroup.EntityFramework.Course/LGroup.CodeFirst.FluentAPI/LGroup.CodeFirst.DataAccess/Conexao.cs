using System;
using System.Data.Entity;

//subimos a pasta de mapeamento para memória
using LGroup.CodeFirst.DataAccess.Mapping;

using LGroup.CodeFirst.Model;

namespace LGroup.CodeFirst.DataAccess
{
    public sealed class Conexao : DbContext
    {
        public Conexao() : base("Data Source=AndreCarlos-msi\\Desenv2008;Initial Catalog=FLUENT;Integrated Security=true")
        {

        }

        public void CriarBanco()
        {
            Database.Create();
        }

        public void ExcluirBanco() 
        {
            Database.Delete();
        }

        //Para visualizar as tabelas, temos que criar propriedades do tipo DBSet
        //para fazer CRUD tem que utilizar dbset, um set para cada tabela
        //colocar o nome da classe que queremos manipular
        public DbSet<PedidoModel> Pedidos { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }

        public DbSet<ItemPedidoModel> ItensPedido { get; set; }

        public DbSet <ClienteModel> Clientes { get; set; }


        //Para criar as tabelas junto com a criação do banco de dados, temos que importar os mapeamentos(MAPPING)
        //dentro da conexao. Temos que sobrescrever um método interno do EF
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Foi nesse momento que colocamos os mapementos na conexao
            //esse comando OnModelcreating eh um comando interno do EF
            //esse comando eh disparado automaticamente assim que for montar o banco
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new ItemPedidoMapping());
            modelBuilder.Configurations.Add(new PedidoMapping());
            modelBuilder.Configurations.Add(new ProdutoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
