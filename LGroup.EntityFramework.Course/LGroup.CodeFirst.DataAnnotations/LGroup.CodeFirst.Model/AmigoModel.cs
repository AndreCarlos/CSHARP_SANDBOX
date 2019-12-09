using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LGroup.CodeFirst.Model
{
    [Table("TB_AMIGO")]
   public class AmigoModel
    {
        [Key]
        [Column("ID_AMIGO", TypeName = "INT")]
       public Int32 Codigo { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("NM_AMIGO", TypeName = "VARCHAR")]
       public String Nome { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("DS_EMAIL", TypeName = "VARCHAR")]
       public String Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("NR_TELEFONE", TypeName = "VARCHAR")]
       public String Telefone { get; set; }

        [Required]
        [Column("DT_NASCIMENTO", TypeName = "DATE")]
       public DateTime DataNascimento { get; set; }

        [Required]
        [Column("ID_SEXO", TypeName = "INT")]
       public Int32 CodigoSexo { get; set; }

        [Required]
        [Column("ID_ESTADO_CIVIL", TypeName = "INT")]
        public Int32 CodigoEstadoCivil { get; set; }

        //estamos gerando o banco via code first utilizando a forma 
        //data annotations temos eque montar agora os relacionamentos
        //entre as tabelas 
        //fizemos uma composiçao entre as classes
        // da mesma forma que as classes se relacionam temos que montar os relacionamento entre as classes
        //aqui no data annotations temos 2 limitações
        //1. Nao tem como fazer a precisao de campo decimal
        //2. Nao tem como fazer relacionamento N para N
        //Esses dois recursos só da pra fazer com code first usando FLUENT API

        [ForeignKey("CodigoSexo")]
        public SexoModel Sexo { get; set;}

        [ForeignKey("CodigoEstadoCivil")]
        public EstadoCivilModel EstadoCivil { get; set; }
    }
}
