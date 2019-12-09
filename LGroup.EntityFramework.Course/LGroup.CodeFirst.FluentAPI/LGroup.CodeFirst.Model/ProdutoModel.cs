using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LGroup.CodeFirst.Model
{
    public sealed class ProdutoModel : Core.BaseModel     //não pode fazer herança da classe
    {
        public String Nome { get; set; }

        public String Descricao { get; set; }

        public Decimal Valor { get; set; }

        public Boolean Ativo { get; set; }

        public String Fornecedor { get; set; }

        public String Categoria { get; set; }
    }
}
