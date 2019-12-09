using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.DataAccessObject.DataAccessLayer.Contracts.Base
{
    /// <summary>
    /// Quando falamos de coleçoes temos 3 coleçoes principais .NET
    /// IENUMERABLE = Lista somente leitura
    /// IQUERYABLE = Lista leitura e pesquisa
    /// ILIST = Lista leitura, pesquisa e também gravação. Desde .Net 2.0
    /// ICOLLECTION = Alternativa mais rescente, moderna, leve ao IList. Desde .Net 4.0
    /// </summary>
    /// <typeparam name="TModelo"></typeparam>
    public interface ILeituraDAO<TModelo>
    {
        IEnumerable<TModelo > Listar();
    }
}
