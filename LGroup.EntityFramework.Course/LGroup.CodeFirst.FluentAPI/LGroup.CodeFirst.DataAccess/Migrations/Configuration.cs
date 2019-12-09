namespace LGroup.CodeFirst.DataAccess.Migrations
{
    using LGroup.CodeFirst.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LGroup.CodeFirst.DataAccess.Conexao>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LGroup.CodeFirst.DataAccess.Conexao";
        }

        //Entity Framework code first migrations é um recurso que existe desde o o EF 4.3, rodo em cima
        //do CODE FIRST, não é nada visual, eh tudo via power shell (nuget comand prompt). O objetivo
        // do migrations eh fazer backup das tabelas (nome e campos) como se fosse um checkibn de código
        //fonte, ele vai versionando as tabelas

        //Sempre que mexer em uma classe de modelo, fazer o mapeamento e gerar o backup
        //add-migration NOMEDOMIGRATION
        //para aplicar o backup (migration) no banco update-database
        //se quiser visualizar o tsql colocar -verbose
        //a tabela migrationhistory guarda todos os backups que aplicamos no banco
        protected override void Seed(LGroup.CodeFirst.DataAccess.Conexao context)
        {
            //o comando seed apareceu quando habilitamos o migration. Ele server
            //para dar uma carga inicial nas tabelas - banco
            //sempre que aplicamos o backup (update-database) eh disparado o comando SEED

            var novoProduto = new ProdutoModel();

            novoProduto.Ativo = true;
            novoProduto.Categoria = "Eletrônico";
            novoProduto.Descricao = "Produtos para Cama";
            novoProduto.Fornecedor = "Connectiion Bahamas";
            novoProduto.Nome = "Xampu";
            novoProduto.Valor = 30;

            //No comando SEED não precisa do SAVECHANGES
            //tomar cuidade para nao duplicar os dados, cada update-database
            //roda o SEED vai inserir novamente
            //o comando addorupdate soh tem no migrations(seed)
            //se nao existir INSERT
            //se já existe UPDATE
            context.Produtos.AddOrUpdate(x => x.Nome, novoProduto);

            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
