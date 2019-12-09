using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// subimos a namespace do EF para poder visualizar as classes de conexao e mapeamento de tabelas
using System.Data.Entity;

///subimos a pasta de mapeamentos para memoria para poder visualizar os arquivos de geração de tables
using LGroup.SuperSisAmigos.DataAcces.Mappings;

///para poder visualizar as classes que viraram tabelas (entidades) subimos para memória esta namespace
using LGroup.SuperSisAmigos.Entities;


namespace LGroup.SuperSisAmigos.DataAcces
{
  /// <summary>
  /// Infraestructure tambem eh um nome do DDD
  /// toda manipulacao de banco de dados, arquivos e pastas, protocolos, tudo isso eh infra-estrutura. É um nome 
  /// bonito que usamos quando manipulamos recursos de rede, windos, da máquina
  /// 
  /// Pra fazer o acesso a dados (conexao com o banco) o que tem de mais top é o EF (Entity Framework)
  /// o EF -> É uma ferramenta de mapeamento e acesso a dados
  /// tem uma estratégia (uma forma de usar) o EF chamada CODE FIRST 
  /// a aplicação vai se encarregar de montar o banco de dados
  /// 
  /// baixamos do nuget a versao mais rescente do EF (6.1.3). Quando baixamos pelo nuget, ele baixa somente o KERNEL
  /// as DLLs internas do EF, quando utilizamos CODE FIRST não tem EDMX, nao tem nada visual, é tudo no pêlo(na raça)
  /// 
  /// A classe dbcontext é a classe de conexao do EF. Equivaleria a classe SQLConnection
  /// </summary>
    public sealed class Conexao : DbContext
    {
        /// <summary>
        /// as string (informacao de conexao) ficam no construtor. Quando for acionadoo o construtor da conexao ele automaticamento
        /// aciona o construtor da classe pai 
        /// </summary>
        public Conexao() : base(@"Data Source=AndreCarlos-msi\Desenv2008;Initial Catalog=SUPERAMIGOS;Integrated Security=true;")
        {

        }

        /// criamos 2 comandos (1 monta e outro deleta) o banco
        public void GerarBanco()
        {
            Database.Create();
        }

        public void DeletarBanco()
        {
            Database.Delete();
        }

        ///o EF nao executa sozinho os arquivos de construção de tabelas. Temos que manualmente  mandar executar
        ///aqueles arquivos 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ///internamente o EF for gerar o banco (create database), ele dispara este comando
            ///OnModelCreating. Esse comando server para colcoar os mapeamentos (definiçoes) das tables
            ///e aqui que ele faz o CREATE TABLE

            modelBuilder.Configurations.Add(new SexoMapping());
            modelBuilder.Configurations.Add(new EstadoCivilMapping());
            modelBuilder.Configurations.Add(new AmigoMapping());
           
           base.OnModelCreating(modelBuilder);
        }

        /// a classe dbcontext é para fazer conexao e equivaleria ao sqlconnection
        /// a classe dbset é para acessar as tabelas, para fazer o crud 
        /// Equivaleria ao DATATABLE ou SQLDATAREADER
        /// Cada tabela tem que ter um dbset, uma classe que a acessa 
        /// 
        ///estamos manipulando os dados da tabela TB_AMIGO == AMIGO
        
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<EstadoCivil> Civis { get; set; }
    }
}