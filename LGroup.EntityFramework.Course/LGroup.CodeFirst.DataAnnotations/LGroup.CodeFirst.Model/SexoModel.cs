using System;

//para poder definir o nome das tabelas, nome dos campos, tamanho dos campos
//subimos para memoria a DLL de Anotacoes (configuracoes adicionais)
//REQUIRED AND MAXLENGHT
using System.ComponentModel.DataAnnotations;

//TABLE, COLUMN, FOREIGN KEY, KEY
using System.ComponentModel.DataAnnotations.Schema;

namespace LGroup.CodeFirst.Model
{
    //a estrategia (forma de mapear o banco) preferida pelos desenvolvedores eh o code first
    //(primeiro desenhamos as classes e depois geramos o banco e as tabelas
    //em cima das Classes) == engenharia reversa
    //cada classe de modelo vai virar uma tabela la no bd
    //eh uma estrategia nao visual(nao tem EDMX, nao tem diagramas, nao tem EF Designer). Existe desde 2011 == EF 4.1
    //podemos utilizar o code first de duas formas:
    //DATA ANNOTATIONS => MAIS SIMPLES (FAGOT), colchetes [] em cima dos campos
    //FLUENT API => MAIS SENIOR (COM MAIS RECUROS), usamos expressoes lambdas (x => x.)

    //na hora do Run essa classe vai virar uma tabela chamada TB_SEXO
    [Table ("TB_SEXO")]
    public class SexoModel
    {
        //Para as propriedades virarem columas das tabelas utilizamos a anatocao COLUMN
        //o key eh chave primaria 
        [Key]
        [Column("ID_SEXO", TypeName = "INT")]
        public Int32 Codigo { get; set; }

        //Colocamos diversar anotacoes em cima dos campos, a ordem tanto faz
        //Required -> not null (nao pode ficar em branco)
        //maxlenght -> tamanho maximo de caracteres
        [Required]
        [MaxLength(9)]
        [Column("DS_SEXO", TypeName = "VARCHAR")]
        public String Descricao { get; set; }
    }
}
