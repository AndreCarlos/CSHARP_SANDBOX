using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LGroup.SisClientes.DataAccessObject.Mappings;
using LGroup.SisClientes.DataTransferObject;

namespace LGroup.SisClientes.DataAccessObject
{
    public sealed class Conexao : DbContext
    {
        public Conexao(): base("Data Source=SAMSUNG3-PC\\SQLEXPRESS; Initial Catalog=PADROES; Integrated Security=True;")
        {

        }

        public DbSet<ClienteDTO> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
