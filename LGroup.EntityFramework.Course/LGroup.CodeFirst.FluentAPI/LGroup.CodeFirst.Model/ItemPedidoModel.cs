using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.CodeFirst.Model
{
    public sealed class ItemPedidoModel : Core.BaseModel
    {
        public Int32 CodigoPedido { get; set; }

        public Int32 CodigoProduto { get; set; }

        public Int32 Quantidade { get; set; }

        //Navigation Properties
        public PedidoModel Pedido { get; set; }

        public ProdutoModel Produto { get; set; }
    }
}
