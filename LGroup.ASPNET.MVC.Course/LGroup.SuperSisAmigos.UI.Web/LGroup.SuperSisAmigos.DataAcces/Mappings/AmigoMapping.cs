using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// essa namespace vai importar as entidades de dominio 
using LGroup.SuperSisAmigos.Entities;

/// essa namespace vai habilitar as classes que vao nos auxiliar a fazer a transformaçao de classe em tables
using System.Data.Entity.ModelConfiguration;

namespace LGroup.SuperSisAmigos.DataAcces.Mappings
{
    public sealed class AmigoMapping : EntityTypeConfiguration<Amigo>
    {
        public AmigoMapping()
        {
            ToTable("TB_AMIGO");

            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_AMIGO").HasColumnType("INT");
            Property(x => x.Nome).HasColumnName("NM_AMIGO").HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
            Property(x => x.Email).HasColumnName("DS_EMAIL").HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
            Property(x => x.Telefone).HasColumnName("NR_TELEFONE").HasColumnType("VARCHAR").HasMaxLength(15).IsRequired();
            Property(x => x.DataNascimento).HasColumnName("DT_NASCIMENTO").HasColumnType("DATE").IsRequired();

            ///fizemos o mapeamento dos campos que virão de outras tabelas (inner join) para paegar a descrição
            Property(x => x.CodigoSexo).HasColumnName("ID_SEXO").HasColumnType("INT").IsRequired();
            Property(x => x.CodigoEstadoCivil).HasColumnName("ID_ESTADO_CIVIL").HasColumnType("INT").IsRequired();

            ///Depois de mapear os dois campos, geramos as foreign Keys(chaves estrangeiras) para as tables
            ///de Sexo e Estado Civil (FK)
            ///
            ///HASREQUIRED -> relacionamento obrigatório. Para ter um amigo, ele precisa ter um sexo
            ///WITHMANY -> Cardinalidade, vários amigos para o mesmo sexo
            ///HASFOREIGNKEY -> Campo que vamos relacionar as tabelas
            ///HASREQUIRED -> EQUIVALE AO INNER JOIN
            ///HASOPTIONAL -> EQUIVALE AO LEFT JOIN
            HasRequired(x => x.Sexo).WithMany().HasForeignKey(x => x.CodigoSexo);
            HasRequired(x => x.EstadoCivil).WithMany().HasForeignKey(x => x.CodigoEstadoCivil);

            //Property(x =>x.Endereco).HasColumnName("DS_ENDERECO").HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
            //Property(x => x.Numero).HasColumnName("NR_RESIDENCIA").HasColumnType("VARCHAR").HasMaxLength(5).IsRequired();
            //Property(x => x.CEP).HasColumnName("NR_CEP").HasColumnType("VARCHAR").HasMaxLength(9).IsRequired();
        }
    }
}
