using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.DataAccessObject.Model;
using System.Data.Entity.ModelConfiguration;

namespace LGroup.DataAccessObject.DataAccessLayer.Mappings
{
    public sealed class AmigoMapping : EntityTypeConfiguration<AmigoModel>
    {
        public AmigoMapping()
        {
            ToTable("TB_AMIGO");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_AMIGO")
                                    .HasColumnType("INT");

            Property(X => X.Nome).HasColumnName("NM_AMIGO")
                                       .HasColumnType("VARCHAR")
                                       .HasMaxLength(50)
                                       .IsRequired();

            Property(X => X.Email).HasColumnName("DS_EMAIL")
                                       .HasColumnType("VARCHAR")
                                       .HasMaxLength(50)
                                       .IsRequired();

            Property(X => X.DataNascimento).HasColumnName("DT_NASCIMENTO")
                                       .HasColumnType("DATE")
                                       .IsRequired();

            Property(X => X.Salario).HasColumnName("VL_SALARIO")
                                       .HasColumnType("DECIMAL")
                                       .HasPrecision(18,2)
                                       .IsRequired();

            Property(X => X.CodigoEstadoCivil).HasColumnName("ID_ESTADO_CIVIL")
                                       .HasColumnType("INT")
                                       .IsRequired();

            Property(X => X.CodigoSexo).HasColumnName("ID_SEXO")
                                      .HasColumnType("INT")
                                      .IsRequired();

            ///criamos os relacionamentos as FOREIGN KEYS (chaves estrangeiras)
            ///HasRequired -> 1 pra 1 ou 1 pra N
            ///HasMany -> N pra N
            HasRequired(x => x.Sexo)
                       .WithMany()
                       .HasForeignKey(x => x.CodigoSexo);

            HasRequired(x => x.EstadoCivil)
           .WithMany()
           .HasForeignKey(x => x.CodigoEstadoCivil);


        }
    }
}
