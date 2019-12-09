using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//subimos para memoria o projeto de modelos (classes de armazenamento de dados )
using LGroup.CodeFirst.Model;

//submios para memoria a namespace que vai habilitar as classe que vao nos auxiliar 
//com o mapeamento das tabelas 
using System.Data.Entity.ModelConfiguration;

namespace LGroup.CodeFirst.DataAccess.Mapping
{
    public sealed class PedidoMapping : EntityTypeConfiguration<PedidoModel>
    {
          public PedidoMapping()
        {
            //definimos o nome da tabela (pedidomodel vai virar tb_pedido)
            ToTable("TB_PEDIDO");

            //chave primaria (PK)
            HasKey(x => x.Codigo);

            //fizemos o mapeamnto dos campos (nome, tamamnho, campo em branco ou nao)
            Property(x => x.Codigo).HasColumnName("ID_PEDIDO")
                                    .HasColumnType("INT");

            Property(x => x.CodigoCliente).HasColumnName("ID_CLIENTE")
                                    .HasColumnType("INT")
                                    .IsRequired();

            Property(x => x.DataCadastro).HasColumnName("DT_CADASTRO")
                                   .HasColumnType("DATETIME")
                                   .IsRequired();

            Property(x => x.ValorTotal).HasColumnName("VL_TOTAL")
                                   .HasColumnType("DECIMAL")
                                   .HasPrecision(8,2)
                                   .IsRequired();

              //Eh aqui que fazemos a geraçao do relacionamento - FK
              //HasRequirede, falamos que vamos ter um relacionamento obrigatorio
              //WithMany, falamos que teremos vario spedidos pra 1 cliente
              //HasForeignKey, falamos que o nome do cmapo que vamos relacionar
              //internamente ele vai cirar um relacionamento com a table TB_CLIENTE
              //atraves do cmapo ID_CLEINTE 
            HasRequired(x => x.Cliente).WithMany().HasForeignKey(x => x.CodigoCliente);                    
        }
    }
}
