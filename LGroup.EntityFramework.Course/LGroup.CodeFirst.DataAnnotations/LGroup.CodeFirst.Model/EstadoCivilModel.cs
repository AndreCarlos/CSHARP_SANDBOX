using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LGroup.CodeFirst.Model
{
    [Table("TB_ESTADO_CIVIL")]
    public class EstadoCivilModel
    {
        [Key]
        [Column("ID_ESTADO_CIVIL", TypeName = "INT")]
        public Int32 Codigo { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("DS_ESTADO_CIVIL", TypeName = "VARCHAR")]
        public String Descricao { get; set; }

    }
}
