using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.CodeFirst.Model;

namespace LGroup.CodeFirst.Repository.Contracts
{
    public interface IProdutoRepository : Core.ILeitura<ProdutoModel>,
                                          Core.IGravacao<ProdutoModel>
    {

    }
}
