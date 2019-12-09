using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.CodeFirst.Model
{
   public sealed class PedidoModel : Core.BaseModel
    {
       public DateTime DataCadastro { get; set; }

       //Esse campo vai virar o ID_CLIENTE
       public Int32 CodigoCliente { get; set; }

       public decimal ValorTotal { get; set; }

       //criamos uma propriedade que representa que vai virar 
       //umna chave estrangeira, que vai virar o relacionamento entre as classes
       //PEDIDO x CLIENTE
       //DataAnnotation [ForeignKey]

       public ClienteModel Cliente { get; set; }
    }
}
