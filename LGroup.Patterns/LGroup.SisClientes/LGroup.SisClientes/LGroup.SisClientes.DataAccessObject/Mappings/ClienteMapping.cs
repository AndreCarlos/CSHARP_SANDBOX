using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LGroup.SisClientes.DataTransferObject;

namespace LGroup.SisClientes.DataAccessObject.Mappings
{
    public sealed class ClienteMapping : EntityTypeConfiguration<ClienteDTO>
    {
        public ClienteMapping()
        {
            ToTable("TB_CLIENTE");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_CLIENTE")
                                   .HasColumnType("INT");

            Property(x => x.Nome).HasColumnName("NM_CLIENTE")
                                 .HasColumnType("VARCHAR")
                                 .HasMaxLength(30)
                                 .IsRequired();

            Property(x => x.Email).HasColumnName("DS_EMAIL")
                                  .HasColumnType("VARCHAR")
                                  .HasMaxLength(30)
                                  .IsRequired();

            Property(x => x.Telefone).HasColumnName("NR_TELEFONE")
                                     .HasColumnType("VARCHAR")
                                     .HasMaxLength(15)
                                     .IsRequired();
        }
    }
}
