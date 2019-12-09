using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Estamos utilizando o EF com a estratégia (forma de mapear o banco)
///CODE FIRST (estrategi nao visual, só tem código, nao tem EDMX)
///mais dificil, demora mais
///Existe desde 2012
///Serve tanto para um novo banco (criado) quando para um banco legado(já existe)
///2 formas de code first 
///DATA ANNOTATIONS
///FLUENT API
///Mais Difícil, mais rescente, preferente dos programadores
///fazemos o mapemento das tabelas em forma de lambdas (expressoes)
///nao tem limitaçoes (dá para fazer tudo)
///

using LGroup.DataAccessObject.Model;
using System.Data.Entity.ModelConfiguration;

namespace LGroup.DataAccessObject.DataAccessLayer.Mappings
{
    /// <summary>
    /// Nos arquivos de mapeamento ficam as configurações de tabelas e campos
    /// nome das tabelas, nome e tamanho e tipo dos campos
    /// cada uma das classes de mapeamento para UMA TABELA
    /// </summary>
    public sealed class SexoMapping : EntityTypeConfiguration<SexoModel>
    {
        /// <summary>
        /// FLUENT API
        /// </summary>
        public SexoMapping()
        {
            ToTable("TB_SEXO");
            HasKey(x => x.Codigo);

            Property(x => x.Codigo).HasColumnName("ID_SEXO")
                                    .HasColumnType("INT");

            Property(X => X.Descricao).HasColumnName("DS_DESC")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(09)
                                        .IsRequired();
        }
    }
}
