using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using LGroup.DataAccessObject.Model;

namespace LGroup.DataAccessObject.DataAccessLayer.Implementations
{
    public sealed class EstadoCivilDAO : Contracts.IEstadoCivilDAO
    {
        public IEnumerable<EstadoCivilModel> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
