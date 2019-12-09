using System;

//subimos para memoria o projeto de modelos (classes de armazenamento de dados)
using LGroup.CodeFirst.Model;

//Subimos para memoria a namespace que vai habilitar as classe que vao nos auxiliar 
//com o mapeamento das tabelas 
using System.Data.Entity.ModelConfiguration;

namespace LGroup.CodeFirst.DataAccess.Mapping
{
    //eh no arquivo de mapeamento que fazemos o mapeamento das 
    //tabelas e campos utilizando o FLUENT API (LAMBDAS)
    public sealed class ClienteMapping : EntityTypeConfiguration<ClienteModel>  //SEALED para ganho de performance e evitar erros de HERANÇA
    {
        public ClienteMapping()
        {
            //definimos o nome da tabela (clientemodel vai virar tb_cliente)
            ToTable("TB_CLIENTE");

            //chave primaria (PK)
            HasKey(x => x.Codigo);

            //fazemos o mapeamento dos campos (nome, tamamnho, campo em branco ou nao)
            Property(x => x.Codigo).HasColumnName("ID_CLIENTE")
                                    .HasColumnType("INT");

            Property(x => x.Nome).HasColumnName("NM_CLIENTE")
                                    .HasColumnType("VARCHAR")
                                    .HasMaxLength(35)
                                    .IsRequired();

            Property(x => x.Email).HasColumnName("DS_EMAIL")
                                   .HasColumnType("VARCHAR")
                                   .HasMaxLength(30)
                                   .IsRequired();

            Property(x => x.DataNascimento).HasColumnName("DT_NASCIMENTO")
                                   .HasColumnType("DATE")
                                   .IsRequired();

            //NOVOS CAMPOS - NUGET MIGRATIONS
            Property(x => x.Endereco).HasColumnName("DS_ENDERECO")
                              .HasColumnType("VARCHAR")
                              .HasMaxLength(300)
                              .IsRequired();

            Property(x => x.CPF).HasColumnName("NR_CPF")
                              .HasColumnType("VARCHAR")
                              .HasMaxLength(15)
                              .IsRequired();
        }
    }
}