using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.DataAccessObject.Model;
using System.Data.Entity.ModelConfiguration;

namespace LGroup.DataAccessObject.DataAccessLayer.Mappings
{
    public sealed class EstadoCivilMapping : EntityTypeConfiguration<EstadoCivilModel>
    {
        public EstadoCivilMapping()
        {
            ToTable("TB_ESTADO_CIVIL");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_ESTADO_CIVIL")
                                    .HasColumnType("INT");

            Property(X => X.Descricao).HasColumnName("DS_DESC")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(09)
                                        .IsRequired();

        }
    }
}
