using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities.Repository
{
    public interface IAmigoRepository : Base.ILeituraRepository<Amigo>, 
                                        Base.IGravacaoRepository<Amigo>
    {

    }
}
