using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using LGroup.DataAccessObject.Model;

namespace LGroup.DataAccessObject.DataAccessLayer.Implementations
{
    /// <summary>
    /// cada classe herda de uma interface
    /// </summary>
    public sealed class SexoDAO : Contracts.ISexoDAO
    {
        public IEnumerable<SexoModel> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
