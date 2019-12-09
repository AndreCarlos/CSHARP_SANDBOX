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
    /// <summary>
    /// cada entidade de dominio vai ser transformada em uma tabela 
    /// essa transformaçao fica dentro dos arquivos de mapeamento. 
    /// É aqui dentro que vamos definir o nome da table, nome dos campos, o tamanho dos campos, o tipo dos campos
    /// todas as configurações de geraçao de table ficam dentro dos arquivos de mapeamento
    /// 
    /// a entidade sexo vai virar una tabela chamada TB_SEXO
    /// </summary>
    public sealed class SexoMapping : EntityTypeConfiguration<Sexo>
    {
        /// <summary>
        /// é no construtor que fazemos o mapeamento da table a dos campos, esse mapeamento var ser feito com FLUENT API
        /// o mapeamento vai ser feito em forma de expressoes lambdas, x => x
        /// </summary>
        public SexoMapping()
        {
            ///tudo isso é mapeamento com lambdas (fluent api)
            ToTable("TB_SEXO");

            ///quando mandarmos executar, o campo codigo vai virar primary key e 
            ///identity (auto incremento)
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_SEXO").HasColumnType("INT");
            Property(x => x.Descricao).HasColumnName("DS_SEXO").HasColumnType("VARCHAR").HasMaxLength(9).IsRequired();
        }
    }
}
