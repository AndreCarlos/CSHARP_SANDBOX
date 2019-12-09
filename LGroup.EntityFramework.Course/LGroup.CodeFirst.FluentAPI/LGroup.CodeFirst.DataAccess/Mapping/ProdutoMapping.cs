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
    //eh no arquivo de mapeamento que fazemos o mapeamento das 
    //tabelas e campos utilizando o FLUENT API (LAMBDAS)
    public sealed class ProdutoMapping : EntityTypeConfiguration<ProdutoModel>
    {
        public ProdutoMapping()
        {
            //definimos o nome da tabela (produtomodel vai virar tb_produto)
            ToTable("TB_PRODUTO");

            //chave primaria (PK)
            HasKey(x => x.Codigo);

            //fizemos o mapeamnto dos campos (nome, tamamnho, campo em branco ou nao)
            Property(x => x.Codigo).HasColumnName("ID_PRODUTO")
                                   .HasColumnType("INT");

            Property(x => x.Nome).HasColumnName("NM_PRODUTO")
                                 .HasColumnType("VARCHAR")
                                 .HasMaxLength(40)
                                 .IsRequired();

            Property(x => x.Descricao).HasColumnName("NM_DESCRICAO")
                                   .HasColumnType("VARCHAR")
                                   .HasMaxLength(200)
                                   .IsRequired();

            Property(x => x.Valor).HasColumnName("VL_PRODUTO")
                                   .HasColumnType("DECIMAL")
                                   .HasPrecision(6,2)
                                   .IsRequired();

            Property(x => x.Ativo).HasColumnName("FLG_STATUS")
                                   .HasColumnType("BIT")
                                   .IsRequired();

            //NOVOS CAMPOS MIGRATIONS
            Property(x => x.Fornecedor).HasColumnName("NM_FORNECEDOR")
                                   .HasColumnType("VARCHAR")
                                   .HasMaxLength(50)
                                   .IsRequired();

            Property(x => x.Categoria).HasColumnName("NM_CATEGORIA")
                                   .HasColumnType("VARCHAR")
                                   .HasMaxLength(15)
                                   .IsRequired();
        }
    }
}
