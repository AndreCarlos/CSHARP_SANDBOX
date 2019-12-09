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
    public sealed class ItemPedidoMapping : EntityTypeConfiguration<ItemPedidoModel>
    {
         public ItemPedidoMapping()
        {
            //definimos o nome da tabela (itempedidomodel vai virar tb_item_pedido)
            ToTable("TB_ITEM_PEDIDO");

            //chave primaria (PK)
            HasKey(x => x.Codigo);

            //fizemos o mapeamnto dos campos (nome, tamamnho, campo em branco ou nao)
            Property(x => x.Codigo).HasColumnName("ID_ITEM_PEDIDO")
                                   .HasColumnType("INT");

            Property(x => x.CodigoPedido).HasColumnName("ID_PEDIDO")
                                    .HasColumnType("INT")
                                    .IsRequired();

            Property(x => x.CodigoProduto).HasColumnName("ID_PRODUTO")
                                   .HasColumnType("INT")
                                   .IsRequired();

            Property(x => x.Quantidade).HasColumnName("NR_QUANTIDADE")
                                   .HasColumnType("INT")
                                   .IsRequired();

            HasRequired(x => x.Pedido).WithMany().HasForeignKey(x => x.CodigoPedido);
            HasRequired(x => x.Produto).WithMany().HasForeignKey(x => x.CodigoProduto);
        }
    }
}
