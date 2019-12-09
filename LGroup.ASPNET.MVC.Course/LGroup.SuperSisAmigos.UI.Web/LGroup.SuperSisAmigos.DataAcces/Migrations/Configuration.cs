namespace LGroup.SuperSisAmigos.DataAcces.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LGroup.SuperSisAmigos.Entities;

    /// <summary>
    /// Desde o EF 4.3 (2012) temos um recurso do CODE FIRST chamado CODE FIRST MIGRATIONS
    /// com ele conseguimos fazer backups da estrutura das tabelas, nome das tabelas, nome dos campos,
    /// tamanho dos campos
    /// 
    /// O backup do sql server ele grava dados e estrutura
    /// O backup do code first (migrations) grava somente ESTRUTURA
    /// Sempre antes de alterar o banco, gere um backup, faz a alteração e aplica no banco
    /// Migrations não é nada visual, é tudo via power shell
    /// Para habilitar o migrations e gerar um backup inicial do banco é o comando ENABLE-MIGRATIONS
    /// 
    /// Faz as mudandças, gera o backup e aplica no banco
    /// para gerar backup das mudanças: ADD-MIGRATION <NOME>
    /// para aplicar um bakcup com as mudanças: UPDATE-DATABASE
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<LGroup.SuperSisAmigos.DataAcces.Conexao>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LGroup.SuperSisAmigos.DataAcces.Conexao";
        }

        /// <summary>
        /// Para voltar um determinado backup, para uma determinada versão
        /// UPDATE-DATABASE -TARGETMIGRATION:NOMEBACKUP
        /// 
        /// Sempre que aplicamos um migration(backup) ele sempre passa no comando SEED (Running Seed Mehtod) até quando 
        /// não tem passa 
        /// o comando SEED serve para dar uma carga inicial nas tabelas
        /// Assim que aplicar o backup insira dados nas tabelas
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(LGroup.SuperSisAmigos.DataAcces.Conexao context)
        {
            /// já deixamos pré confiugrados alguns sexos e estados civis
            /// já deixamos uma carga inicial nas tabelas
            /// 
            ///umn problema do SEED eh que sempre que rodamos o bakcup 
            ///UPDATE-DATABASE ele executa o SEED, toda hora ele ira inserir sexos e estados civis e os dados ficariam
            ///duplicados. Para executar uma unica vez o insert colocar o comando ADDORUPDATE e no primeiro parametro
            ///mandar o lambda falando o campo que ele tem que dar um WHERE
            context.Sexos.AddOrUpdate(x=>x.Descricao, new Sexo { Descricao = "Feminino" });
            context.Sexos.AddOrUpdate(x => x.Descricao, new Sexo { Descricao = "Masculino" });

            context.Civis.AddOrUpdate(x => x.Descricao, new EstadoCivil { Descricao = "Casado" });
            context.Civis.AddOrUpdate(x => x.Descricao, new EstadoCivil { Descricao = "Divorciado" });
            context.Civis.AddOrUpdate(x => x.Descricao, new EstadoCivil { Descricao = "Solteiro" });
            context.Civis.AddOrUpdate(x => x.Descricao, new EstadoCivil { Descricao = "Viúvo" });

            context.Amigos.AddOrUpdate(x => x.Nome, new Amigo { Nome = "Zina", 
                                                                Email="zina@ig.com.br",
                                                                DataNascimento = DateTime.Now,
                                                                Telefone = "987654321",
                                                                CodigoEstadoCivil = 9,
                                                                CodigoSexo = 6
                                                               });


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
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
