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
    public sealed class EstadoCivilMapping : EntityTypeConfiguration<EstadoCivil>
    {
        public EstadoCivilMapping()
        {
            ToTable("TB_ESTADO_CIVIL");

            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_ESTADO_CIVIL").HasColumnType("INT");
            Property(x => x.Descricao).HasColumnName("DS_ESTADO_CIVIL").HasColumnType("VARCHAR").HasMaxLength(15).IsRequired();
        }
    }
}
